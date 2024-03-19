using InternetBankingApp.Core.Application.ViewModels.Transaccion;

namespace InternetBankingApp.Core.Application.Interfaces.Customer
{
    public interface IAgregarTransferencia
    {
        Task AgregarTransaccion(SaveTransaccionViewModel vm);
    }
}