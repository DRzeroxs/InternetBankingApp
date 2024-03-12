using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.ViewModels.User;

namespace InternetBankingApp.Core.Application.Interfaces.IAccount;

public interface IAccountServices
{
    Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
    Task<string> ConfirmAccountAsync(string userId, string token);
    Task<RegistrerResponse> RegistrerAdminUserAsync(RegistrerRequest request, string origin);
    Task<RegistrerResponse> RegistrerCustomerUserAsync(RegistrerRequest request, string origin);
    Task SingOutAsync();
    Task<List<UserViewModel>> GetAllUserAsync();
}