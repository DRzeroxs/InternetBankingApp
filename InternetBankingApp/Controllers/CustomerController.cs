using AutoMapper;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.Products;
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
        private readonly IDashBoardService _dashBoardService;
        public CustomerController(IUserService userService, IMapper mapper,
            ICuentaDeAhorroService cuentaAhorroService, IClienteService clienteService
            ,ITarjetaDeCreditoService tarjetaDeCreditoService, IPrestamoService prestamoService, IDashBoardService dashBoardService)
        {
            _userService = userService;
            _mapper = mapper;
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;
            _tarjetaDeCreditoService = tarjetaDeCreditoService;
            _prestamoService = prestamoService;
            _dashBoardService = dashBoardService;
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

            SaveCuentaDeAhorroViewModel cuentaSave = new()
            {
                Balance = Balance,
                ClientId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(await _dashBoardService.GetAllIdentifiersAsync()),
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


            SaveTarjetaDeCreditoViewModel tarjetaSave = new()
            {
                Limit = Balance,
                ClienteId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(await _dashBoardService.GetAllIdentifiersAsync()),
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

            SavePrestamoViewModel tarjetaSave = new()
            {
                InitialDebt = Balance,
                ClienteId = cliente.Id,
                Identifier = IdentifierGenerator.GenerateCode(await _dashBoardService.GetAllIdentifiersAsync()),
                CurrentDebt = Balance
            };

            await _prestamoService.AddAsync(tarjetaSave);

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
          
        }


        public async Task<IActionResult> DeleteProduct(string userId)
        {
            ClienteViewModel cliente = await _clienteService.GetByIdentityId(userId);

            ProductViewModel products = await _dashBoardService.GetAllProductsByClientIdAsync(cliente.Id);

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int identifier, EProducts type)
        {
            if(type == EProducts.CuentaDeAhorro)
            {
                CuentaDeAhorroViewModel cuentaVm = await _cuentaAhorroService.GetByIdentifier(identifier);

                if (cuentaVm != null && cuentaVm.Id > 0 && !cuentaVm.Main)
                {

                    await _cuentaAhorroService.Eliminar(cuentaVm.Id);
                }
            }
            
            if(type == EProducts.TarjetaDeCredito)
            {
                TarjetaDeCreditoViewModel tarjetaVm = await _tarjetaDeCreditoService.GetByIdentifier(identifier);

                if (tarjetaVm != null && tarjetaVm.Id > 0 && tarjetaVm.Debt <= 0)
                {
                    await _tarjetaDeCreditoService.Eliminar(tarjetaVm.Id);
                }
            }
            
            if(type == EProducts.Prestamo)
            {
                PrestamoViewModel prestamoVm = await _prestamoService.GetByIdentifier(identifier);

                if (prestamoVm != null && prestamoVm.Id > 0 && prestamoVm.CurrentDebt <= 0)
                {
                    await _prestamoService.Eliminar(prestamoVm.Id);
                }
            }


            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
        }
    }
}
