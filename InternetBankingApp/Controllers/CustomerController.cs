using AutoMapper;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Application.ViewModels.User;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InternetBankingApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICuentaDeAhorroService _cuentaAhorroService;
        private readonly IClienteService _clienteService;   
        private readonly ITarjetaDeCreditoService _tarjetaDeCreditoService; 
        private readonly IPrestamoService _prestamoService; 
        public CustomerController(IUserService userService, IMapper mapper,
            ICuentaDeAhorroService cuentaAhorroService, IClienteService clienteService
            ,ITarjetaDeCreditoService tarjetaDeCreditoService, IPrestamoService prestamoService)
        {
            _userService = userService;
            _mapper = mapper;
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;
            _tarjetaDeCreditoService = tarjetaDeCreditoService;
            _prestamoService = prestamoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            EditClientViewModel userSave = _mapper.Map<EditClientViewModel>(user);

            return View(userSave);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditClientViewModel vm)
        {

            await _userService.EditUserCustomerAsync(vm);

            return RedirectToAction("Index", "Administrator" , await _userService.GetAllUser());
        }
        public async Task<IActionResult> AddProduct(string userId)
        {
           

            return View("AddProduct", userId);
        }

        public async Task<IActionResult> AddCuentaAhorro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCuentaAhorro(double Balance, string userId)
        {
           var cliente = await _clienteService.GetByIdentityId(userId);

            List<int> identifier = await _cuentaAhorroService.GetAllIdentifiers();

            SaveCuentaDeAhorroViewModel cuentaSave = new()
            {
                Balance = Balance,
                ClientId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(identifier),
                Main = false
            };

            await _cuentaAhorroService.AddAsync(cuentaSave);

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
        }

        public async Task<IActionResult> AddTarjetaCredito()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddTarjetaCredito(double Balance, string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);

            List<int> identifier = await _tarjetaDeCreditoService.GetAllIdentifiers();

            SaveTarjetaDeCreditoViewModel tarjetaSave = new()
            {
                Limit = Balance,
                ClienteId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(identifier),
                Debt = 0
            };

            await _tarjetaDeCreditoService.AddAsync(tarjetaSave);    

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
        }
        public async Task<IActionResult> AddCuentaPrestamo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCuentaPrestamo(double Balance, string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);

            List<int> identifier = await _prestamoService.GetAllIdentifiers();

            SavePrestamoViewModel tarjetaSave = new()
            {
                InitialDebt = Balance,
                ClienteId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(identifier),
                CurrentDebt = Balance
            };

            await _prestamoService.AddAsync(tarjetaSave);

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
          
        }
    }
}
