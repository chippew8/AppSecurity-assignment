using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WebApplication3.ViewModels
{
    public class Register
    {
		[Required]
		[DataType(DataType.Text)]
		[RegularExpression(@"^[a-zA-Z0-9_ ]*$", ErrorMessage = "Full Name cannot have special characters")]
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
		[RegularExpression(@" ^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$ ", ErrorMessage = "Phone number is Invalid")]
		public string PhoneNumber { get; set; }
		[Required]
		[DataType(DataType.Text)]
		[RegularExpression("(\\d{1,3}.)?.+\\s(\\d{6})$", ErrorMessage ="Invalid Address")]
		public string DeliveryAddress { get; set; }
		[Required]
        [DataType(DataType.EmailAddress)]
		[RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Invalid Email Address")]
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
