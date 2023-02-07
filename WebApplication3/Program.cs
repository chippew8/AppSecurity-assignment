using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Identity;
using WebApplication3;
using WebApplication3.Core;
using WebApplication3.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AuthDbContext>();

builder.Services.Configure<GoogleCaptchaConfig>(builder.Configuration.GetSection("GoogleReCaptcha"));
builder.Services.AddTransient(typeof(GoogleCaptchaService));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
	options =>
	{
		options.SignIn.RequireConfirmedAccount= false;
		options.Lockout.AllowedForNewUsers= true;
		options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromSeconds(2);
		options.Lockout.MaxFailedAccessAttempts = 3;
	}).AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
	// Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequiredLength = 12;
	options.Password.RequiredUniqueChars = 0;

	// Lockout settings.
	options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 3;
	options.Lockout.AllowedForNewUsers= true;

    // User settings
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(1);

    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Encryption Services
builder.Services.AddDataProtection();
// Sessions Services
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(30);
});



var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseStatusCodePagesWithReExecute("/errors/{0}");
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
