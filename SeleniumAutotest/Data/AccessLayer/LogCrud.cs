using SeleniumAutotest.Data.Models;

namespace SeleniumAutotest.Data.AccessLayer
{
    public class LogCrud : ICrud<Log, Guid>
    {
        protected ApplicationDbContext dbcontext;
        public LogCrud(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public Log Create(Log model)
        {
            dbcontext.Add(model);
            dbcontext.SaveChanges();
            return model;
        }

        public void Delete(Log model)
        {
            dbcontext.Remove(model);
            dbcontext.SaveChanges();
        }

        public List<Log> Get()
        {
            return dbcontext.Logs.OrderByDescending(x => x.CreatedAt).ToList();
        }

        public List<Log> Get(Func<Log, bool> predicate)
        {
            return dbcontext.Logs.Where(predicate).ToList();
        }

        public Log GetById(Guid Id)
        {
            return dbcontext.Logs.FirstOrDefault(x => x.Id == Id);
        }

        public void Update(Log model)
        {
            dbcontext.Update(model);
            dbcontext.SaveChanges();
        }
    }
}
