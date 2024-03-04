using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace SeleniumAutotest.Core
{
    public enum Browser
    {
        //IE,
        Firefox,
        Chrome
    }

    public class WebDriverFactory
    {
        IConfiguration Configuration;

        public WebDriverFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public WebDriver InitBrowser(Browser browser)
        {           
            var path = Configuration["Drivers:Path"];
            var browserName = browser.ToString();
            WebDriver driver = null;
            switch (browserName)
            {
                case "Firefox":
                    var fireFoxOptions = new FirefoxOptions();
                    if (HeadLess())                    
                        fireFoxOptions.AddArgument("headless");                                    
                    driver = new FirefoxDriver(path, fireFoxOptions);
                    break;
                case "IE":
                    var ieOptions = new InternetExplorerOptions();
                    if (HeadLess())
                        ieOptions.AddAdditionalOption("headless", "true");
                    driver = new InternetExplorerDriver(path, ieOptions);
                    break;
                case "Chrome":
                    var chromeOptions = new ChromeOptions();
                    if (HeadLess())
                        chromeOptions.AddArgument("headless");
                    driver = new ChromeDriver(path, chromeOptions);
                    break;
                default:
                    throw new Exception("Impossible to intit browser " + browserName);
            }

            driver.Manage().Window.Maximize();

            return driver;
        }

        bool HeadLess()
        {
            var setting = Configuration["HeadLess"];
            if (bool.TryParse(setting, out bool result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }
    }
}
