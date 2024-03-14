using AutoMapper;
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
        private readonly IMapper _mapper;
        public AdministratorController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            await ActiveAndInactiveUsers();
            return View();
        }

     
        public async Task <IActionResult> UsersManagement()
        {
            var user = await _userService.GetAllUser();

            return View(user);
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
                vm.StartAmount = 0.0;
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

            return View("UsersManagement",await  _userService.GetAllUser());
        }

        public async Task<IActionResult> ActiveUser(string UserId)
        {
            var user = await _userService.GetByUserId(UserId);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ActiveUserPost(ActiveInactiveViewModel vm)
        {
            await _userService.ConfirnUserAsync(vm.Id);

            await ActiveAndInactiveUsers();

            return RedirectToAction("Index", await _userService.GetAllUser());
        }

        public async Task<IActionResult> InactiveUser(string userId)
        {
            var user = await _userService.GetByUserId(userId);

            return View(user);  
        }

        [HttpPost]
        public async Task<IActionResult> InactiveUserPost(ActiveInactiveViewModel vm)
        {
            await _userService.InactiveUserAsync(vm.Id);

            await ActiveAndInactiveUsers();
            
            return View("Index", await _userService.GetAllUser());
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            EditAdminViewModel userSave = _mapper.Map<EditAdminViewModel>(user);

            return View(userSave);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditAdminViewModel vm)
        {

            await _userService.EditUserdminAsync(vm);

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
        }
        private async Task ActiveAndInactiveUsers()
        {
            ViewBag.UserActive = await _userService.CountUsersActiveAsync();
            ViewBag.UserInactive = await _userService.CountUsersIActiveAsync();
        }
    }
}
