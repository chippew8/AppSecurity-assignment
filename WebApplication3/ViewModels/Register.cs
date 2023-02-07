using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
		[Required]
		[DataType(DataType.Text)]
		[RegularExpression(@"^[a-z,.'-]+$/i", ErrorMessage = "Cannot have special characters in Full Name")]
		public string FullName { get; set; }
		[Required]
		[DataType(DataType.CreditCard)]
		[RegularExpression(@"[0-9]{16,16}",
		 ErrorMessage = "Credit Card is Invalid")]
		public string CreditCardNo { get; set; }
		[Required]
		[DataType(DataType.Text)]
		public string Gender { get; set; }
		[Required]
		[DataType(DataType.PhoneNumber)]
		[RegularExpression(@"[0-9]{8,8}", ErrorMessage = "Phone number is Invalid")]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.Text)]
		public string DeliveryAddress { get; set; }
		[Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }

		[Required]
		public IFormFile? PhotoPath { get; set; }

		[Required]
		[DataType(DataType.Text)]
		public string AboutMe { get; set; }
    }
}
