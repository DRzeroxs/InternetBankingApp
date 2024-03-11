using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm);
        Task SignOutAsync();
        Task<RegistrerResponse> RegisterAdminAsync(RegisterViewModel vm, string origin);
        Task<RegistrerResponse> RegisterCustomerAsync(RegisterViewModel vm, string origin);
    }
}
