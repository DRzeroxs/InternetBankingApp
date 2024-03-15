using AutoMapper;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace InternetBankingApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ICuentaDeAhorroService _cuentaAhorroService;
        private readonly IClienteService _clienteService;   
        public CustomerController(IUserService userService, IMapper mapper, ICuentaDeAhorroService cuentaAhorroService, IClienteService clienteService)
        {
            _userService = userService;
            _mapper = mapper;
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;

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
        public async Task<IActionResult> AddTarjetaCredito(SaveCuentaDeAhorroViewModel vm)
        {
            return View();
        }
        public async Task<IActionResult> AddCuentaPrestamo()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddCuentaPrestamo(SaveCuentaDeAhorroViewModel vm)
        {
            return View();
        }
    }
}
