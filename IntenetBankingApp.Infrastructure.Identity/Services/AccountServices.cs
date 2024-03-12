using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Core.Application.ViewModels.User;
using InternetBankingApp.Infrastructure.Identity.Context;
using InternetBankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Identity.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public async Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(requuest.Email);

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {requuest.Email} ";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, requuest.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential For {requuest.Email} ";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Accound No Confirmed for {requuest.Email} ";
                return response;
            }


            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            return response;
        }

        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin)
        {
            RegistrerResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username {request.UserName} is already Taken";

                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"username {request.Email} is already registrer";

                return response;
            }

            var user = new ApplicationUser
            {
                EmailConfirmed = false,
                FirstName = request.FirstName,
                LatsName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                TypeOfUser = request.TypeOfUser,
                IsActive = request.IsActive
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An Error ocurred trying to register the user";

                return response;
            }

            return response;
        }

        public async Task<RegistrerResponse> RegistrerCustomerUserAsync(RegistrerRequest request, string origin)
        {
            RegistrerResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username {request.UserName} is already Taken";

                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"username {request.Email} is already registrer";

                return response;
            }

            var user = new ApplicationUser
            {
                EmailConfirmed = false,
                FirstName = request.FirstName,
                LatsName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                TypeOfUser = request.TypeOfUser,
                IsActive = request.IsActive,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Customer.ToString());
            }
            else
            {
                response.HasError = true;
                response.Error = $"An Error ocurred trying to register the user";

                return response;
            }

            return response;
        }

        public async Task ConfirmAccountAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);  

            user.IsActive = true;

           var result = await _userManager.UpdateAsync(user);
        }

        public async Task InactiveAccountAsync(string userId)
        {

            var user = await _userManager.FindByIdAsync(userId);

            user.IsActive = false;

            var result = await _userManager.UpdateAsync(user);
        }
        public async Task<UserViewModel> GetById(string Id)
        {
            var u = await _userManager.FindByIdAsync(Id);

            UserViewModel UserVm = new()
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LatsName,
                UserName = u.UserName,
                Email = u.Email,
                TypeOfUser = u.TypeOfUser,
                IsActive = u.IsActive
            };
            return UserVm;
               
        }
        public async Task<List<UserViewModel>> GetAllUserAsync()
        {
            var userList = await _userManager.Users.ToListAsync();
           
            return userList.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LatsName,
                UserName = u.UserName,
                Email = u.Email,
                TypeOfUser = u.TypeOfUser,
                IsActive = u.IsActive
                
            }).ToList();
        }
    }
}
