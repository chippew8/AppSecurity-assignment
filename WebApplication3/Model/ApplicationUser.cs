using Microsoft.AspNetCore.Identity;

namespace WebApplication3.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null;
        public string CreditCardNo { get; set; } = null;
        public string Gender { get; set; } = null;
        public string DeliveryAddress { get; set; } = null;
        public string photo_path { get; set; } = null;
        public string aboutMe { get; set; } = null;

    }
}
