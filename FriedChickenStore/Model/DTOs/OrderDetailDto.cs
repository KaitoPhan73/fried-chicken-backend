using System.Text.Json.Serialization;

namespace FriedChickenStore.Model.DTOs
{
    public class OrderDetailDto : BaseDTO
    {
        public int Quantity { get; set; }
        public Double price { get; set; }
        public int ProductId { get; set; }
        public ProductDto? Product { get; set; }

        public int OrderId { get; set; }
        public OrderDto? Order { get; set; }
    }
}
