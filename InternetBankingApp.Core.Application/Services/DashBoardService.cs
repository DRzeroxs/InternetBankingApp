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
        private readonly ITransaccionRepository _transaccionRepository;
        private readonly IPrestamoService _prestamoService;
        private readonly IUserService _userService;

        public DashBoardService(ICuentaDeAhorroService cuentaService, ITarjetaDeCreditoService tarjetaService, IPrestamoService prestamoService, IUserService userService, ITransaccionRepository transaccionRepository)
        {
            _cuentaService = cuentaService;
            _tarjetaService = tarjetaService;
            _prestamoService = prestamoService;
            _userService = userService;
            _transaccionRepository = transaccionRepository;
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
                PayToday = await _transaccionRepository.CuentasDePagoHoy(),
                PayInitial = await _transaccionRepository.CuentasDePago(),
                TransactionsToday = await _transaccionRepository.CuentraTransaccionesHoy(),
                TransactionsInitial = await _transaccionRepository.CuentaDeTransacciones(),
                ProductsCount = await GetAllIdentifiersAsync(),
            };

            return dashBoardViewModel;
        }

        public async Task<ProductViewModel> GetAllProductsByClientIdAsync(int clienteId)
        {

            ProductViewModel products = new ProductViewModel() { 
                cuentas = await _cuentaService.GetProductViewModelByClientId(clienteId),
                tarjetas = await _tarjetaService.GetProductViewModelByClientId(clienteId),
                prestamos = await _prestamoService.GetProductViewModelByClientId(clienteId),
            }; 

            return products;
        }
    }
}
