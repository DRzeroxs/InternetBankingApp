using InternetBankingApp.Core.Domain.Entities;

namespace InternetBankingApp.Core.Application.Interfaces.IRepository
{
    public interface ITransaccionRepository : IGenericRepository<Transaccion>
    {
        Task<int> CuentaDeTransacciones();
        Task<int> CuentraTransaccionesHoy();
        Task<int> CuentasDePago();
        Task<int> CuentasDePagoHoy();
    }
}
