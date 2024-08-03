using FriedChickenStore.CoreHelper;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriedChickenStore.Model.Entity
{
    public class Order : BaseEntity
    {
        
        public String OrderStatus { get; set; }

        public Double price { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
