using OpenQA.Selenium;

using SeleniumAutotest.Data;
using SeleniumAutotest.Data.AccessLayer;

using System.Threading.Tasks;
using System.Text;

namespace SeleniumAutotest.Core.Scenarios
{
    public class ScenarioExecutor : WebDriverClientBase
    {
        ScenarioActionCrud actionsCrud;

        public ScenarioExecutor(IConfiguration configuration, Browser browser, ApplicationDbContext dbContext) 
            : base(configuration, browser)
        {
            actionsCrud = new ScenarioActionCrud(dbContext);
        }

        public string Execute(Scenario scenario) => _Execute(scenario, this.actionsCrud);

        public string Execute(Scenario scenario, ScenarioActionCrud actionsCrud) => _Execute(scenario, actionsCrud);

        string _Execute(Scenario scenario, ScenarioActionCrud scenarioActionCrud)
        {
            var actionsDto = scenario.ScenarioActions ?? scenarioActionCrud.Get(x => x.ScenarioId == scenario.Id);

            if (actionsDto == null || actionsDto.Count == 0)
            {
                return $"No actions in scenario {scenario.Title}";
            }

            string message = $"Scenario {scenario.Title} started. Driver: {this.Driver.GetType().Name} <br>";
            var builder = new StringBuilder(message);
            foreach (var item in actionsDto)
            {
                var result = TryExecute(item);
                builder.AppendLine(result.Message + " <br>");

                if (!result.Success && !item.ContinueOnError)
                {
                    break;
                }

                Task.Delay(item.DelayMilliseconds <= 0 ? 300 : item.DelayMilliseconds).Wait();
            }

            var resultNote = builder.ToString();
            return resultNote;
        }
        public void Quit()
        {           
            Driver.Quit();
            Driver.Dispose();
        }

        private (string Message, bool Success) TryExecute(ScenarioAction action)
        {
            try
            {
                action.AsAction().Invoke(Driver);
                return ($"Action {action.Name}={action.Value} passed successfully ", true);
            }
            catch(Exception exc)
            {
                return ($"Action {action.Name}={action.Value} passed with error {exc.Message} ", false);
            }      
        }
    }
}
