using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumAutotest.Core
{
    public class WebDriverClientBase
    {
        protected WebDriver Driver;

        public WebDriverClientBase(IConfiguration configuration, Browser browser)
        {
            var factory = new WebDriverFactory(configuration);
            Driver = factory.InitBrowser(browser);
        }
    }
}
