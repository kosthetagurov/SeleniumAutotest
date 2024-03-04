using System.Linq;
using SeleniumAutotest.Data;

namespace SeleniumAutotest.Core.Scenarios
{
    public class ScenarioExecutorFacade
    {
        private IConfiguration configuration;
        private ApplicationDbContext dbContext;

        public ScenarioExecutorFacade(IConfiguration configuration, ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public List<ScenarioExecutor> GetScenarioExecutors(params string[] browsers)
        {
            if (browsers.Length == 0)
            {
                throw new Exception("No browser specified");
            }

            var parsedBrowsers = browsers
                .ToList()
                .Select(x => Enum.Parse(typeof(Browser), x))
                .Cast<Browser>()
                .OrderBy(x => x)
                .ToList();

            var executors = new List<ScenarioExecutor>();
            parsedBrowsers.ForEach(x => executors.Add(new(configuration, x, dbContext)));
            return executors;
        }

        public ScenarioExecutor GetChrome()
        {
            return new ScenarioExecutor(configuration, Browser.Chrome, dbContext);            
        }

        public ScenarioExecutor GetFirefox()
        {
            return new ScenarioExecutor(configuration, Browser.Firefox, dbContext);            
        }
    }
}
