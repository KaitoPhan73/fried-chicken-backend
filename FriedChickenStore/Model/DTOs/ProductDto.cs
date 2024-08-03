using FriedChickenStore.CoreHelper;

namespace FriedChickenStore.Model.DTOs
{
    public class ProductDto : BaseDTO
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public String? Type { get; set; }
    }

}
