using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using System.Text.Json.Nodes;
using WebApplication3.Core;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {

        [BindProperty]
        public Login LModel { get; set; }
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IHttpContextAccessor contxt;
        private readonly GoogleCaptchaService _captchaService;

        public LoginModel(SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, GoogleCaptchaService service)
		{
			this.signInManager = signInManager;
            contxt = httpContextAccessor;
            _captchaService = service;
        }
		public void OnGet()
        {
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            var captchaResult = await _captchaService.VerifyToken(LModel.Token);
            if (!captchaResult)
            {
                return Page();
            }

            if(ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
                if (identityResult.Succeeded)
                {
                    contxt.HttpContext.Session.SetString("Name", LModel.Email);
                    
                    return RedirectToPage("Index");
                }else if(identityResult.IsLockedOut)
                {
                    ModelState.AddModelError("", "The account is locked out");
                    return Page();
                }
            }

            
            ModelState.AddModelError("", "Username or Password incorrect");
            return Page();
        }

		
	}
}
