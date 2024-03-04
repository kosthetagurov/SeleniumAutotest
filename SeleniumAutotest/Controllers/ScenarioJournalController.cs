using Microsoft.AspNetCore.Mvc;

using SeleniumAutotest.Core.Scenarios;
using SeleniumAutotest.Data;
using SeleniumAutotest.Data.AccessLayer;

namespace SeleniumAutotest.Controllers
{
    public class ScenarioJournalController : Controller
    {
        ScenarioJournalCrud scenarioJournalCrud;
        public ScenarioJournalController(ApplicationDbContext dbContext)
        {
            scenarioJournalCrud = new ScenarioJournalCrud(dbContext);
        }

        [Route("/api/scenario/journal/{id}")]
        public List<ScenarioJournal> GetScenarioJournals(Guid id)
        {
            return scenarioJournalCrud.Get(x => x.ScenarioId == id);
        }
    }
}
