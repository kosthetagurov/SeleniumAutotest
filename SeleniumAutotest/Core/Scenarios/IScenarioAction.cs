using OpenQA.Selenium;

namespace SeleniumAutotest.Core.Scenarios
{
    public interface IScenarioAction
    {
        void Click(WebDriver driver);
        void Navigate(WebDriver driver);
        void WriteTo(WebDriver driver);
        void Select(WebDriver driver);
    }
}
