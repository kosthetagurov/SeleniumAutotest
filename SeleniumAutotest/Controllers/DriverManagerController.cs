using Microsoft.AspNetCore.Mvc;

using SeleniumAutotest.Extensions;
using SeleniumAutotest.Infrastructure;
using SeleniumAutotest.ViewModels;

namespace SeleniumAutotest.Controllers
{
    public class DriverManagerController : Controller
    {
        public static Dictionary<string, string> DriverNames => new Dictionary<string, string>()
        {
            ["Chrome"] = "chromedriver.exe",
            ["Firefox"] = "geckodriver.exe",
            ["IE"] = "IEDriverServer.exe"
        };

        Logger logger;
        IConfiguration configuration;
        public DriverManagerController(IConfiguration configuration, Logger logger)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        [Route("/drivers")]
        public IActionResult Index()
        {
            var path = configuration["Drivers:Path"];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var model = new DriverManagerViewModel()
            {
                Drivers = DriverNames.Select(x => new DriverInfo
                {
                    Name = x.Key,
                    File = x.Value,
                    Exists = System.IO.File.Exists(Path.Combine(path, x.Value))
                }).ToList()
            };
            return View(model);
        }

        [Route("/api/drivers/upload/{type}")]
        [HttpPost]
        public async Task<IActionResult> UploadDriver(string type)
        {
            try
            {
                var file = Request.Form.Files[0];

                if (file.FileName != DriverNames[type])
                {
                    throw new Exception($"{file.FileName} is not {type} driver");
                }
                
                if (file.Length == 0)
                {
                    throw new Exception("Empty file");
                }

                var path = configuration["Drivers:Path"];
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string filePath = Path.Combine(path, file.FileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await file.CopyToAsync(fileStream);

                return Ok();
            }
            catch (Exception exc)
            {
                logger.Log(exc);
                Response.StatusCode = 500;
                return new ObjectResult(exc.HtmlSummary());
            }            
        }
    }
}
