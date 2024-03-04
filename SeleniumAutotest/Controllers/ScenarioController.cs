using Microsoft.AspNetCore.Mvc;

using SeleniumAutotest.Core.Scenarios;
using SeleniumAutotest.Data;
using SeleniumAutotest.Data.AccessLayer;
using SeleniumAutotest.Extensions;
using SeleniumAutotest.Infrastructure;

namespace SeleniumAutotest.Controllers
{
    public class ScenarioController : Controller
    {
        Logger logger;

        ApplicationDbContext dbContext;

        ScenarioCrud scenarioCrud;
        ScenarioJournalCrud scenarioJournalCrud;

        IConfiguration configuration;
        ApplicationDbContextFactory applicationDbContextFactory;
        
        public ScenarioController(ApplicationDbContext dbContext,
            IConfiguration configuration,
            ApplicationDbContextFactory applicationDbContextFactory,            
            Logger logger)
        {
            this.logger = logger;
            this.applicationDbContextFactory = applicationDbContextFactory;
            this.configuration = configuration;
            this.dbContext = dbContext;

            scenarioJournalCrud = new ScenarioJournalCrud(dbContext);
            scenarioCrud = new ScenarioCrud(dbContext);
        }

        [Route("/scenarios/common")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/api/scenarios")]
        [HttpGet]
        public List<Scenario> GetScenarios()
        {
            return scenarioCrud.Get().OrderByDescending(x => x.CreatedAt).ToList();
        }

        [Route("/api/scenario/new")]
        public IActionResult New(string? name)
        {
            name = name ?? "New scenario " + DateTime.Now;
            var model = scenarioCrud.Create(new Scenario
            {
                Title = name,
            });
            return RedirectToAction("Edit", "Scenario", new { id = model.Id });
        }

        [Route("/scenario/{id}")]
        public IActionResult Edit(Guid id)
        {
            var model = scenarioCrud.GetById(id);
            return View(model);
        }

        [Route("/api/scenario/update")]
        [HttpPost]
        public IActionResult UpdateScenario(Scenario model)
        {
            scenarioCrud.Update(model);
            Response.StatusCode = 200;
            return new EmptyResult();
        }

        [Route("/api/scenarios/destroy")]
        [HttpPost]
        public IActionResult DeleteScenario([FromForm] Scenario scenario)
        {
            scenarioCrud.Delete(scenario);

            Response.StatusCode = 200;
            return new EmptyResult();
        }

        [Route("/api/scenario/execute")]
        public IActionResult ExecuteScenario(ExecutionOptions options)
        {
            try
            {
                if (!options.IsValid(out string message))
                {
                    Response.StatusCode = 500;
                    return new ObjectResult(message);
                }

                var scenario = scenarioCrud.GetById(options.ScenarioId);
                var actionsCrud = new ScenarioActionCrud(dbContext);

                // Параллельное выполнение пока не реализовано. Существует ошибка переполнения стека
                if (options.Parallel)
                {
                    
                }
                else
                {
                    var executors = new ScenarioExecutorFacade(configuration, dbContext).GetScenarioExecutors(options.Browsers);

                    foreach (var e in executors)
                    {
                        for (var i = 0; i < options.Iterations; i++)
                        {
                            var result = e.Execute(scenario);

                            scenarioJournalCrud.Create(new ScenarioJournal()
                            {
                                ScenarioId = scenario.Id,
                                Message = result
                            });
                            dbContext.SaveChanges();

                            Task.Delay(100);
                        }
                        e.Quit();
                    }
                }

                Response.StatusCode = 200;
                return new EmptyResult();
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
