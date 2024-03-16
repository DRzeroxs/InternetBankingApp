using InternetBankingApp.Core.Application.ViewModels.DashBoard;
using InternetBankingApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface IDashBoardService
    {
        Task<List<int>> GetAllIdentifiersAsync();
        Task<DashBoardViewModel> GetDashBoard();
        Task<ProductViewModel> GetAllProductsAsync();
    }
}
