namespace FriedChickenStore.Model.DTOs
{
    public class UserDto : BaseDTO
    {
        public String? UserName { get; set; }
        public String? Password { get; set; }
        public String? Email { get; set; }
        public String? Phone { get; set; }
        public String? Address { get; set; }
        public String? Role { get; set; }
    }
}
