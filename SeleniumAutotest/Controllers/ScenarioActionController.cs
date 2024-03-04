using Microsoft.AspNetCore.Mvc;

using SeleniumAutotest.Core.Scenarios;
using SeleniumAutotest.Data;
using SeleniumAutotest.Data.AccessLayer;

namespace SeleniumAutotest.Controllers
{
    public class ScenarioActionController : Controller
    {
        ScenarioActionCrud scenarioActionCrud;
        public ScenarioActionController(ApplicationDbContext dbContext)
        {
            scenarioActionCrud = new ScenarioActionCrud(dbContext);
        }

        [Route("/api/scenario/swap/up/{id}")]
        [HttpPost]
        public void Up(int id)
        {
            scenarioActionCrud.SwapUp(id);
            Response.StatusCode = 200;
        }

        [Route("/api/scenario/swap/down/{id}")]
        [HttpPost]
        public void Down(int id)
        {
            scenarioActionCrud.SwapDown(id);
            Response.StatusCode = 200;
        }

        [Route("/api/scenario/addaction")]
        [HttpPost]
        public ScenarioAction AddScenarioAction([FromForm] ScenarioAction action)
        {
            action = scenarioActionCrud.Create(action);
            Response.StatusCode = 200;
            return action;
        }

        [Route("/api/scenario/updateaction")]
        [HttpPost]
        public IActionResult UpdateScenarioAction([FromForm] ScenarioAction action)
        {
            scenarioActionCrud.Update(action);
            Response.StatusCode = 200;
            return new EmptyResult();
        }

        [Route("/api/scenario/deleteaction")]
        [HttpPost]
        public IActionResult DeleteScenarioAction([FromForm] ScenarioAction action)
        {
            scenarioActionCrud.Delete(action);
            Response.StatusCode = 200;
            return new EmptyResult();
        }

        [Route("/api/scenario/actions/{id}")]
        [HttpGet]
        public List<ScenarioAction> GetScenarioActions(Guid id)
        {
            return scenarioActionCrud.Get(x => x.ScenarioId == id).ToList();
        }
    }
}
