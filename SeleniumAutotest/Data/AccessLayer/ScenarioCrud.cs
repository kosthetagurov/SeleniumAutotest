using SeleniumAutotest.Core.Scenarios;

namespace SeleniumAutotest.Data.AccessLayer
{
    public class ScenarioCrud : ICrud<Scenario, Guid>
    {
        ApplicationDbContext dbcontext;

        public ScenarioCrud(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public Scenario Create(Scenario model)
        {
            dbcontext.Add(model);
            dbcontext.SaveChanges();
            return model;
        }

        public void Delete(Scenario model)
        {
            dbcontext.Remove(model);
            dbcontext.SaveChanges();
        }

        public List<Scenario> Get()
        {
            return dbcontext.Scenarios.ToList();
        }

        public List<Scenario> Get(Func<Scenario, bool> predicate)
        {
            return dbcontext.Scenarios.Where(predicate).ToList();
        }

        public Scenario GetById(Guid Id)
        {
            return dbcontext.Scenarios.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(Scenario model)
        {
            dbcontext.Update(model);
            dbcontext.SaveChanges();
        }
    }
}
