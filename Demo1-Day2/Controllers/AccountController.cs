using Demo1_Day2.Controllers.CustomFilters;
using Demo1_Day2.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Mono.TextTemplating;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net;
using System.Security.Claims;

namespace Demo1_Day2.Controllers
{
	public class AccountController : Controller
	{
		private readonly ITIContext db;
		public AccountController(ITIContext context)
		{
			db = context;
		}
		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
			if (user == null)
			{
				ModelState.AddModelError("", "Invalid email or password");
				return View();
			}
			
			Claim claim1 = new Claim(ClaimTypes.Name, user.Name);
			Claim claim2 = new Claim(ClaimTypes.Email, user.Email);
            List<Claim> claims = new List<Claim>();
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }
            ClaimsIdentity claimsIdentity1 = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
			claimsIdentity1.AddClaim(claim1);
			claimsIdentity1.AddClaim(claim2);
            claimsIdentity1.AddClaims(claims);

            ClaimsPrincipal claimsPrincipal1 = new ClaimsPrincipal();
			claimsPrincipal1.AddIdentity(claimsIdentity1);
			await HttpContext.SignInAsync(claimsPrincipal1);
			
			return RedirectToAction("Index", "Department");
		}

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            
			return RedirectToAction("Index", "Department");
        }
    }
}
