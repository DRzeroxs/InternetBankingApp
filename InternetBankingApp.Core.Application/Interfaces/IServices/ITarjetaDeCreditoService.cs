using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface ITarjetaDeCreditoService : IGenericService<SaveTarjetaDeCreditoViewModel, TarjetaDeCreditoViewModel, TarjetaDeCredito>
    {

    }
}
