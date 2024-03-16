using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.DashBoard;
using InternetBankingApp.Core.Application.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class DashBoardService : IDashBoardService
    {
        private readonly ICuentaDeAhorroService _cuentaService;
        private readonly ITarjetaDeCreditoService _tarjetaService;
        private readonly IPrestamoService _prestamoService;
        private readonly IUserService _userService;

        public DashBoardService(ICuentaDeAhorroService cuentaService, ITarjetaDeCreditoService tarjetaService, IPrestamoService prestamoService, IUserService userService)
        {
            _cuentaService = cuentaService;
            _tarjetaService = tarjetaService;
            _prestamoService = prestamoService;
            _userService = userService;
        }

        public async Task<List<int>> GetAllIdentifiersAsync()
        {
            // Espera a que cada tarea se complete secuencialmente
            List<int> identifiers = new List<int>();

            identifiers.AddRange(await _cuentaService.GetAllIdentifiers());
            identifiers.AddRange(await _tarjetaService.GetAllIdentifiers());
            identifiers.AddRange(await _prestamoService.GetAllIdentifiers());

            return identifiers;
        }

        public async Task<DashBoardViewModel> GetDashBoard()
        {
            var dashBoardViewModel = new DashBoardViewModel()
            {
                ActiveUsers = await _userService.CountUsersActiveAsync(),
                InactiveUsers = await _userService.CountUsersIActiveAsync(),
                PayToday = 0,
                PayInitial = 0,
                TransactionsToday = 0,
                TransactionsInitial = 0,
                ProductsCount = await GetAllIdentifiersAsync(),
            };

            return dashBoardViewModel;
        }

        public async Task<ProductViewModel> GetAllProductsAsync()
        {

            ProductViewModel products = new ProductViewModel() { 
                cuentas = await _cuentaService.GetAllAsync(),
                tarjetas = await _tarjetaService.GetAllAsync(),
                prestamos = await _prestamoService.GetAllAsync(),
            }; 

            return products;
        }
    }
}
