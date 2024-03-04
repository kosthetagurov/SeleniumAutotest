using Microsoft.EntityFrameworkCore;
using SeleniumAutotest.Data;
using SeleniumAutotest.Data.AccessLayer;
using SeleniumAutotest.Data.Models;
using SeleniumAutotest.Extensions;

namespace SeleniumAutotest.Infrastructure
{
    public class Logger : LogCrud
    {
        public Logger(ApplicationDbContext dbcontext) 
            : base(dbcontext)
        {
        }

        public void Log(Log log) => Create(log);

        public void Log(Exception exception)
        {
            Create(new Log()
            {
                Type = LogType.Exception,
                IsSuccess = false,
                Message = exception.Summary()
            });
        }

        public void ClearLogs()
        {
            var sql = "TRUNCATE TABLE " + nameof(ApplicationDbContext.Logs);
            dbcontext.Database.ExecuteSqlRaw(sql);
        }
    }
}
