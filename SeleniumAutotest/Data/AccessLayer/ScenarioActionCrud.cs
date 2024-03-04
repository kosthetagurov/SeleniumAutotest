using SeleniumAutotest.Core.Scenarios;

namespace SeleniumAutotest.Data.AccessLayer
{
    public class ScenarioActionCrud : ICrud<ScenarioAction, int>
    {
        ApplicationDbContext dbcontext;

        public ScenarioActionCrud(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public void SwapUp(int id)
        {
            var model = GetById(id);
            var previous = Get(x => x.ScenarioId == model.ScenarioId && x.OrderId == model.OrderId - 1).FirstOrDefault();
            if (previous != null)
            {
                model.OrderId = model.OrderId - 1;
                previous.OrderId = previous.OrderId + 1;
                dbcontext.SaveChanges();
            }
        }

        public void SwapDown(int id)
        {
            var model = GetById(id);
            var next = Get(x => x.ScenarioId == model.ScenarioId && x.OrderId == model.OrderId + 1).FirstOrDefault();
            if (next != null)
            {
                model.OrderId = model.OrderId + 1;
                next.OrderId = next.OrderId - 1;
                dbcontext.SaveChanges();
            }
        }

        public ScenarioAction Create(ScenarioAction model)
        {
            var actions = dbcontext.ScenarioActions.Where(x => x.ScenarioId == model.ScenarioId).ToList();
            var maxOrderId = actions.Count == 0 ? 0 : actions.Max(x => x.OrderId);
            model.OrderId = maxOrderId + 1;
            dbcontext.Add(model);
            dbcontext.SaveChanges();
            return model;
        }

        public void Delete(ScenarioAction model)
        {
            var actions = dbcontext.ScenarioActions.Where(x => x.ScenarioId == model.ScenarioId && x.OrderId > model.OrderId).ToList();
            actions.ForEach(x => x.OrderId = x.OrderId - 1);
            dbcontext.Remove(model);
            dbcontext.SaveChanges();
        }

        public List<ScenarioAction> Get()
        {
            return dbcontext.ScenarioActions.OrderBy(x => x.Id).ToList();
        }

        public List<ScenarioAction> Get(Func<ScenarioAction, bool> predicate)
        {
            return dbcontext.ScenarioActions.Where(predicate).OrderBy(x => x.OrderId).ToList();
        }

        public ScenarioAction GetById(int Id)
        {
            return dbcontext.ScenarioActions.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(ScenarioAction model)
        {
            dbcontext.Update(model);
            dbcontext.SaveChanges();
        }
    }
}
