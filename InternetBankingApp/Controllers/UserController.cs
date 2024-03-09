using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InternetBankingApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            AuthenticationResponse userAuthenticate = await _userService.LoginAsync(loginViewModel);

            if (userAuthenticate != null && userAuthenticate.HasError != true)
            {
                HttpContext.Session.set<AuthenticationResponse>("user", userAuthenticate);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                loginViewModel.HasError = userAuthenticate.HasError;
                loginViewModel.Error = userAuthenticate.Error;
                return View(loginViewModel);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
