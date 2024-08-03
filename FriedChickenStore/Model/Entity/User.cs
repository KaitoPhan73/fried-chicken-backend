namespace FriedChickenStore.Model.Entity
{
    public class User : BaseEntity
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String Email { get; set; }
        public String Phone {  get; set; }
        public String Address { get; set; }
        public String? Role { get; set; }
    }
}
