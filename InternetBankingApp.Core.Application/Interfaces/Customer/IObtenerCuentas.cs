using InternetBankingApp.Core.Application.ViewModels.Beneficiario;

namespace InternetBankingApp.Core.Application.Interfaces.Customer
{
    public interface IObtenerCuentas
    {
        Task<List<ClientBeneficiaryViewModel>> CuentasBeneficiario(string userId);
        Task<List<int>> CuentasPersonales(string userId);
    }
}