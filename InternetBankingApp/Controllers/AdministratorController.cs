using Azure;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace InternetBankingApp.Controllers
{
    public class AdministratorController : Controller
    {
        private readonly IUserService _userService;
        public AdministratorController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAllUser();
            return View(user);
        }

        public async Task <IActionResult> UsersManagement()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(RegisterViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View(vm);
            }

            if(vm.StartAmount == null)
            {
                vm.StartAmount = "0";
            }

            var origin = Request.Headers["origin"];

            if(vm.TypeOfUser == "Admin")
            {
                RegistrerResponse response = await _userService.RegisterAdminAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }
            else if(vm.TypeOfUser == "Customer")
            {
                RegistrerResponse response = await _userService.RegisterCustomerAsync(vm, origin);

                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }

            return View("UsersManagement");
        }

        public async Task<IActionResult> ConfirnUser(string UserId)
        {
           await _userService.ConfirnUserAsync(UserId);

            return View("Index", await _userService.GetAllUser());
        }
       
        public async Task<IActionResult> InactiveUser(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            return View(user);  
        }

        [HttpPost]
        public async Task<IActionResult> InactiveUserPost(UserViewModel vm)
        {
            await _userService.InactiveUserAsync(vm.Id);

            return View("Index", await _userService.GetAllUser());
        }
    }
}
