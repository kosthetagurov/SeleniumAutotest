using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

using System.ComponentModel.DataAnnotations.Schema;

namespace SeleniumAutotest.Core.Scenarios
{
    [NotMapped]
    public class JsBasedScenarioAction : ScenarioAction
    {
        public override void Click(WebDriver driver)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"$('{Value.Replace('\'', '\"')}').trigger('click');");
        }

        public override void Select(WebDriver driver)
        {           
            var splited = Value.Split("@");
            var selector = splited[0];
            var value = splited[1];

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"$('{selector.Replace('\'', '\"')}').val('{value}');");
        }

        public override void WriteTo(WebDriver driver)
        {
            var selector = Value.Substring(0, Value.IndexOf("@"));
            var value = Value.Substring(Value.IndexOf("@") + 1);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"$('{selector.Replace('\'', '\"')}').val('{value}');");
        }        
    }
}
