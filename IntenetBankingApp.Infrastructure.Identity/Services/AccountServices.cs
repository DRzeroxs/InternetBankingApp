using AutoMapper;
using Azure;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
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
        private readonly IClienteService _clientService;
        private readonly ICuentaDeAhorroService _cuentaAhorro;
        private readonly IMapper _mapper;
        public AccountServices(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IClienteService clientService, ICuentaDeAhorroService cuentaAhorro, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _clientService = clientService;
            _cuentaAhorro = cuentaAhorro;
            _mapper = mapper;
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
                IdentificationCard = request.IdentificationCard,
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
                IdentificationCard = request.IdentificationCard,
                UserName = request.UserName,
                TypeOfUser = request.TypeOfUser,
                IsActive = request.IsActive,
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            SaveClienteViewModel SaveClient = new()
            { 
                FirstName = request.FirstName,  
                LatsName = request.LastName, 
                UserId = user.Id
            };

            var userClient =  await _clientService.AddAsync(SaveClient);


            List<int> CurrentsIdentifiers = await _cuentaAhorro.GetAllIdentifiers();

            SaveCuentaDeAhorroViewModel saveCuenta = new()
            {
                Main = true,
                Balance = (double)request.StartAmount,
                ClientId = userClient.Id,
                Identifier = IdentifierGenerator.GenerateCode(CurrentsIdentifiers),
            };

            await _cuentaAhorro.AddAsync(saveCuenta);

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
                IdentificationCard = u.IdentificationCard,  
                TypeOfUser = u.TypeOfUser,
                IsActive = u.IsActive
            };
            return UserVm;  
        }
        public async Task EditUserCustomerAsync(EditClientViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            var client = await _clientService.GetByIdentityId(user.Id);

            var cuentadeAhorro = await _cuentaAhorro.GetByClientId(client.Id);

            cuentadeAhorro.Balance = cuentadeAhorro.Balance + vm.AddAmount;

            await _cuentaAhorro.Editar(cuentadeAhorro, cuentadeAhorro.Id);

            if (user != null)
            {
                user.Id = vm.Id;
                user.FirstName = vm.FirstName;
                user.LatsName = vm.LastName;
                user.Email = vm.Email;
                user.IdentificationCard = vm.IdentificationCard;    
                user.UserName = vm.UserName;
                user.PasswordHash = await UpdatePassword(user.Id, vm.Password);

                var result =  await _userManager.UpdateAsync(user);   
             
            }
        }
        public async Task EditUserAdminAsync(EditAdminViewModel vm)
        {
            var user = await _userManager.FindByIdAsync(vm.Id);

            if (user != null)
            {
                user.Id = vm.Id;
                user.FirstName = vm.FirstName;
                user.LatsName = vm.LastName;
                user.Email = vm.Email;
                user.UserName = vm.UserName;
                user.PasswordHash = await UpdatePassword(user.Id, vm.Password);

                var result = await _userManager.UpdateAsync(user);

            }
        }
        private async Task<string> UpdatePassword(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);

            return newPasswordHash;
        }
        public async Task<ActiveInactiveViewModel> GetByUserId(string Id)
        {
            var u = await _userManager.FindByIdAsync(Id);

            ActiveInactiveViewModel UserVm = new()
            {
                Id = u.Id,
                IsActive = u.IsActive
            };
            return UserVm;
        }
        public async Task<int> CountActiveUseryAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            int count = 0;

            foreach(var user in userList)
            {
                if(user.IsActive == true)
                {
                    count ++;   
                }
            }

            return count;   
        }

        public async Task<int> CountIActiveUseryAsync()
        {
            var userList = await _userManager.Users.ToListAsync();

            int count = 0;

            foreach (var user in userList)
            {
                if (user.IsActive == false)
                {
                    count++;
                }
            }

            return count;
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
