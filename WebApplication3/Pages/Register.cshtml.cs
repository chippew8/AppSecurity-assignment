using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.Model;
using WebApplication3.ViewModels;
using System.Text.RegularExpressions;
 

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {

        private UserManager<ApplicationUser> userManager { get; }
        private SignInManager<ApplicationUser> signInManager { get; }
        private IWebHostEnvironment env;
		private readonly IHttpContextAccessor contxt;

		[BindProperty]
        public Register RModel { get; set; }


        public RegisterModel(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            env = environment;
			contxt = httpContextAccessor;
		}


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
			
			if (ModelState.IsValid)
            {
                var uploadsFolder = "uploads";
                var imageFile = "";
                var imagePath = "";
                var check = Regex.IsMatch(RModel.PhotoPath.FileName, @".\.(jpg|jpeg|JPG|JPEG)$");
                if (check)
                {
                    if (RModel.PhotoPath != null)
                    {
                        if (RModel.PhotoPath.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("RModel.PhotoPath", "File size cannot exceed 2MB");
                            return Page();
                        }
                        imageFile = Guid.NewGuid() + Path.GetExtension(RModel.PhotoPath.FileName);
                        imagePath = Path.Combine(env.ContentRootPath, "wwwroot", uploadsFolder, imageFile);

                    }

                    var dataProtectionProvider = DataProtectionProvider.Create("EncryptData");
                    var protector = dataProtectionProvider.CreateProtector("MySecretKey");

                    // making app user
                    var user = new ApplicationUser()
                    {
                        UserName = RModel.Email,
                        FullName = RModel.FullName,
                        CreditCardNo = protector.Protect(RModel.CreditCardNo),
                        Gender = RModel.Gender,
                        PhoneNumber = RModel.PhoneNumber,
                        DeliveryAddress = RModel.DeliveryAddress,
                        Email = RModel.Email,
                        photo_path = string.Format("/{0}/{1}", uploadsFolder, imageFile),
                        aboutMe = RModel.AboutMe
                    };
                    var result = await userManager.CreateAsync(user, RModel.Password);
                    if (result.Succeeded)
                    {
                        // upload image
                        using var fileStream = new FileStream(imagePath, FileMode.Create);
                        await RModel.PhotoPath.CopyToAsync(fileStream);

                        await signInManager.SignInAsync(user, false);
						contxt.HttpContext.Session.SetString("Name", RModel.Email);
						return RedirectToPage("Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("RModel.PhotoPath", "File needs to be a jpg file");
                    return Page();
                }
            }
            return Page();
        }







    }
}
