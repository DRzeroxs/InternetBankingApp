using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.ViewModels.User;

namespace InternetBankingApp.Core.Application.Interfaces.IAccount;

public interface IAccountServices
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task ConfirmAccountAsync(string userId);
    Task InactiveAccountAsync(string userId);
    Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerCustomerUserAsync(RegistrerRequest request, string origin);
    Task SingOutAsync();
    Task<List<UserViewModel>> GetAllUserAsync();
    Task<UserViewModel> GetById(string Id);
    Task<ActiveInactiveViewModel> GetByUserId(string Id);

}