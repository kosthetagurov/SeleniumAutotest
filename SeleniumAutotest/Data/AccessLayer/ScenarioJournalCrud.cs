using SeleniumAutotest.Core.Scenarios;

namespace SeleniumAutotest.Data.AccessLayer
{
    public class ScenarioJournalCrud : ICrud<ScenarioJournal, int>
    {
        ApplicationDbContext dbcontext;

        public ScenarioJournalCrud(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public ScenarioJournal Create(ScenarioJournal model)
        {
            dbcontext.Add(model);
            dbcontext.SaveChanges();
            return model;
        }

        public void Delete(ScenarioJournal model)
        {
            dbcontext.Remove(model);
            dbcontext.SaveChanges();
        }

        public List<ScenarioJournal> Get()
        {
            return dbcontext.ScenarioJournals.OrderByDescending(x => x.Id).ToList();
        }

        public List<ScenarioJournal> Get(Func<ScenarioJournal, bool> predicate)
        {
            return dbcontext.ScenarioJournals.OrderByDescending(x => x.Id).Where(predicate).ToList();
        }

        public ScenarioJournal GetById(int Id)
        {
            return dbcontext.ScenarioJournals.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(ScenarioJournal model)
        {
            dbcontext.Update(model);
            dbcontext.SaveChanges();
        }
    }
}
