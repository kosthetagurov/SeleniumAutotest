using Microsoft.AspNetCore.Mvc;

using SeleniumAutotest.Data.Models;
using SeleniumAutotest.Infrastructure;

namespace SeleniumAutotest.Controllers
{
    public class EventsController : Controller
    {
        Logger logger;

        public EventsController(Logger logger)
        {
            this.logger = logger;
        }

        [Route("/events")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/api/events")]
        public List<Log> GetLogs()
        {
            return logger.Get();
        }

        [HttpPost]
        [Route("/api/events/delete")]
        public void Delete(Log log)
        {
            logger.Delete(log);
        }

        [Route("/api/events/clear")]
        public IActionResult ClearLogs()
        {
            logger.ClearLogs();
            return RedirectToAction("Index", "Events");
        }
    }
}
