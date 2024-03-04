using OpenQA.Selenium.Support;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using SeleniumAutotest.Data.AccessLayer;
using System.ComponentModel.DataAnnotations.Schema;
using SeleniumExtras.WaitHelpers;

namespace SeleniumAutotest.Core.Scenarios
{
    public enum ActionType
    {
        /// <summary>
        /// Переход по сслыке
        /// </summary>
        Navigate = 0,
        /// <summary>
        /// Нажатие лкм по элементу (выбранному по CSS селектору)
        /// </summary>
        Click = 1,
        /// <summary>
        /// Ввод текста в текстовое поле (выбранному по CSS селектору)
        /// </summary>
        WriteTo = 3,
        /// <summary>
        /// Выбор элемента из ниспадающего списка (выбранному по CSS селектору)
        /// </summary>
        Select
    }

    public class ScenarioAction : DbModelBase<int>, IScenarioAction
    {
        public ScenarioAction()
        {

        }

        public ScenarioAction(ActionType actionType, string value)
        {
            Name = actionType.ToString();
            Value = value;
        }

        public int DelayMilliseconds { get; set; }
        public bool ContinueOnError { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int OrderId { get; set; }

        public Guid ScenarioId { get; set; }
        public Scenario Scenario { get; set; }

        public Action<WebDriver> AsAction()
        {
            var act = new Action<WebDriver>(driver =>
            {
                var name = Name;
                switch (name)
                {
                    case nameof(ActionType.Navigate):
                        Navigate(driver);
                        break;
                    case nameof(ActionType.Click):
                        Click(driver);
                        break;                    
                    case nameof(ActionType.WriteTo):
                        WriteTo(driver);
                        break;
                    case nameof(ActionType.Select):
                        Select(driver);
                        break;
                    default:
                        throw new NotImplementedException("Can not to execute scenario action");
                }
            });

            return act;
        }

        public virtual void Select(WebDriver driver)
        {
            var splited = Value.Split("@");
            var selector = splited[0];
            var value = splited[1];

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(selector.Trim())));
            var select = new SelectElement(element);
            select.SelectByValue(value);
        }

        public virtual void Navigate(WebDriver driver)
        {
            driver.Navigate().GoToUrl(Value);
        }

        public virtual void Click(WebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(Value.Trim())));
            element.Click();
        }

        public virtual void WriteTo(WebDriver driver)
        {
            var selector = Value.Substring(0, Value.IndexOf("@"));
            var value = Value.Substring(Value.IndexOf("@") + 1);

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            var element = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector(selector.Trim())));
            element.SendKeys(value);
        }
    }
}
