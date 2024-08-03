using System.ComponentModel.DataAnnotations.Schema;

namespace FriedChickenStore.Model.Entity
{
    public class OrderDetail : BaseEntity
    {
        public int Quantity { get; set; }

        public Double price { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
