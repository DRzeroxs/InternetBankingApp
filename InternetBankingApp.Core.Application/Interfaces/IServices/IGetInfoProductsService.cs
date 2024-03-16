using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface IGetInfoProductsService<ViewModel> where ViewModel : class
    {
        Task<ViewModel> GetByIdentifier(int identifier);
        Task<List<int>> GetAllIdentifiers();
        Task<List<ViewModel>> GetProductViewModelByClientId(int clienteId);
    }
}
