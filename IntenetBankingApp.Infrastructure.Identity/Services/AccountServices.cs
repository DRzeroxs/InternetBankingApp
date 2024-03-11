using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
                PhoneNumber = request.PhoneNumber,


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
                PhoneNumber = request.PhoneNumber,

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

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No Account Registrer with this User";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Accound confirmed for {user.Email} You can now use the app";
            }
            else
            {
                return $"An Error ocurred while confirming {user.Email}";
            }
        }
    }
}
