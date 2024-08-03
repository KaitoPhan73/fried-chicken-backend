using FriedChickenStore.CoreHelper;

namespace FriedChickenStore.Model.DTOs
{
    public class OrderDto : BaseDTO
    {
        public String? OrderStatus { get; set; }
        public Double price { get; set; }
        public int? UserId { get; set;}
       
    }
}
