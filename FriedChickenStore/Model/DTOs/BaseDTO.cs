namespace FriedChickenStore.Model.DTOs
{
    public abstract class BaseDTO
    {
        protected BaseDTO()
        {
            Status = true;
        }
        public int Id { get; set; }
        public DateTime CreateTimes { get; set; }
        public DateTime LastUpdateTimes { get; set; }
        public bool Status { get; set; }
    }
}
