using SeleniumAutotest.Data.AccessLayer;

namespace SeleniumAutotest.Core.Scenarios
{
    public class ScenarioJournal : DbModelBase<int>
    {
        //public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public Guid ScenarioId { get; set; }
        public Scenario Scenario { get; set; }
    }
}
