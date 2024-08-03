using System.ComponentModel.DataAnnotations;

namespace FriedChickenStore.Model.Entity
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            CreateTimes = DateTime.Now;
            LastUpdateTimes = DateTime.Now;
            Status = true;
        }
    

        [Key]
        public int Id { get; set; }
        public DateTime CreateTimes { get; set; } 
        public DateTime LastUpdateTimes  { get; set; } 
        public bool Status {  get; set; }
    }
}
