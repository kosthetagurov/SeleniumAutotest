using Newtonsoft.Json;

using SeleniumAutotest.Data.AccessLayer;

namespace SeleniumAutotest.Core.Scenarios
{
    public enum ScenarioType
    {
        Common,
        Passing
    }

    public class Scenario : DbModelBase<Guid>
    {
        public ScenarioType Type { get; set; }
        public string Title { get; set; }        
        public List<ScenarioAction> ScenarioActions { get; set; }
    }
}
