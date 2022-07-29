using System.Security.Claims;
using Auth0.AspNetCore.Authentication;
using BloggingPlatform.DataService.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BloggingPlatform.BusinessService.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/Post")
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        [Authorize]
        public async Task Logout()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult UserProfile()
        {
            return View(new UserProfile()
            {
                Name = User.Identity.Name,
                Email = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                Avatar = User.FindFirst(c => c.Type == "picture")?.Value
            });
        }

    }
}