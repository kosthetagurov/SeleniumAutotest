using System.ComponentModel.DataAnnotations.Schema;

namespace SeleniumAutotest.Data.AccessLayer
{
    public abstract class DbModelBase<TId>
    {
        public DbModelBase()
        {
            CreatedAt = DateTime.Now;            
        }

        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public string DateString => CreatedAt.ToString();
    }
}
