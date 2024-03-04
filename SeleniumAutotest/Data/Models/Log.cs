using SeleniumAutotest.Data.AccessLayer;

namespace SeleniumAutotest.Data.Models
{
    public enum LogType
    {
        Exception,
        Event
    }

    public class Log : DbModelBase<Guid>
    {
        public LogType Type { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public string TypeString => Type.ToString();
    }
}
