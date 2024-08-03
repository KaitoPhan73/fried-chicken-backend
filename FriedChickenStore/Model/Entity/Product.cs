using FriedChickenStore.CoreHelper;

namespace FriedChickenStore.Model.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public String? Type { get; set; }
    }
}
