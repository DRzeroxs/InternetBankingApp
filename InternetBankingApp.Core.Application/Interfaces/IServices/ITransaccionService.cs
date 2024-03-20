using InternetBankingApp.Core.Application.ViewModels.Transaccion;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface ITransaccionService : IGenericService<TransaccionViewModel, SaveTransaccionViewModel, Transaccion>
    {
       //Task PagarTarjetaCredito(SaveTransaccionViewModel sv);
        Task<SaveTransaccionViewModel> PagarTarjetaCredito(SaveTransaccionViewModel sv);
        Task AgregarTransaccion(SaveTransaccionViewModel vm);
      
    }
}
