using AutoMapper;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.Products;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Application.ViewModels.Transaccion;
using InternetBankingApp.Core.Application.ViewModels.User;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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
        private readonly IBeneficiarioService _beneficiarioService;  
        private readonly ITransaccionService _transaccionService;
        public CustomerController(IUserService userService, IMapper mapper,
            ICuentaDeAhorroService cuentaAhorroService, IClienteService clienteService
            ,ITarjetaDeCreditoService tarjetaDeCreditoService, IPrestamoService prestamoService,
            IDashBoardService dashBoardService, IBeneficiarioService beneficiarioService
            ,ITransaccionService transaccionService)
        {
            _userService = userService;
            _mapper = mapper;
            _cuentaAhorroService = cuentaAhorroService;
            _clienteService = clienteService;
            _tarjetaDeCreditoService = tarjetaDeCreditoService;
            _prestamoService = prestamoService;
            _dashBoardService = dashBoardService;
            _beneficiarioService = beneficiarioService;
            _transaccionService = transaccionService;   
        }
        public async Task <IActionResult> Index(string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);  

            var cuentaAhorro = await _cuentaAhorroService.GetProductViewModelByClientId(cliente.Id);

            var prestamo = await _prestamoService.GetProductViewModelByClientId(cliente.Id);

            var tarjetaDeCredito = await _tarjetaDeCreditoService.GetProductViewModelByClientId(cliente.Id);

            List<ProductViewModel> productVm = new();

            productVm.Add(new ProductViewModel
            {
                cuentas = cuentaAhorro,
                prestamos = prestamo,
                tarjetas = tarjetaDeCredito
            });
           
            return View(productVm);
        }

        public async Task<IActionResult> Beneficiary(string userId)
        {
            var client = await _clienteService.GetByIdentityId(userId);

            var beneficiaryList = await _beneficiarioService.GetBeneficiaryList(client.Id); 

            return View(beneficiaryList);  
        }
        public async Task<IActionResult> AddBeneficiary()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBeneficiary(SaveBeneficiarioViewModel vm, string userId)
        {
           if(!ModelState.IsValid)
            {
                return View(new {userId = userId });
            }

            if(vm.CuentaIdentifier == 0 || vm.CuentaIdentifier.ToString().Length < 9)
            {
                ModelState.AddModelError("Empty Identifier field", "An Account number was not entered");
                var clientId = await _clienteService.GetByIdentityId(userId);
                var benficiarios = await _beneficiarioService.GetBeneficiaryList(clientId.Id);

                return View("Beneficiary", benficiarios);
            }

            var client = await _clienteService.GetByIdentityId(userId);
            vm.ClienteId = client.Id;

            await _beneficiarioService.AddAsync(vm);

            var clientI = await _clienteService.GetByIdentityId(userId);

            var beneficiaryList = await _beneficiarioService.GetBeneficiaryList(clientI.Id);

            return View("Beneficiary", beneficiaryList);
        }
        public async Task<IActionResult> DeleteBeneficiary(int Id)
        {
           var beneficiary = await _beneficiarioService.GetById(Id);

            return View(beneficiary);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBeneficiary(SaveBeneficiarioViewModel vm, string userId)
        {
            await _beneficiarioService.Eliminar(vm.Id);

            var client = await _clienteService.GetByIdentityId(userId);

            var beneficiaryList = await _beneficiarioService.GetBeneficiaryList(client.Id);

            return View("Beneficiary", beneficiaryList);
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
            if (type == EProducts.CuentaDeAhorro)
            {
                CuentaDeAhorroViewModel cuentaVm = await _cuentaAhorroService.GetByIdentifier(identifier);

                if (cuentaVm != null && cuentaVm.Id > 0 && !cuentaVm.Main)
                {

                    await _cuentaAhorroService.Eliminar(cuentaVm.Id);
                }
            }

            if (type == EProducts.TarjetaDeCredito)
            {
                TarjetaDeCreditoViewModel tarjetaVm = await _tarjetaDeCreditoService.GetByIdentifier(identifier);

                if (tarjetaVm != null && tarjetaVm.Id > 0 && tarjetaVm.Debt <= 0)
                {
                    await _tarjetaDeCreditoService.Eliminar(tarjetaVm.Id);
                }
            }

            if (type == EProducts.Prestamo)
            {
                PrestamoViewModel prestamoVm = await _prestamoService.GetByIdentifier(identifier);

                if (prestamoVm != null && prestamoVm.Id > 0 && prestamoVm.CurrentDebt <= 0)
                {
                    await _prestamoService.Eliminar(prestamoVm.Id);
                }
            }

            return RedirectToAction("Index", "Administrator", await _userService.GetAllUser());
        }
        public async Task<IActionResult> PagoExpreso(string userId)
        {
            await CuentasPersonales(userId);

            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> PagoExpreso(SaveTransaccionViewModel vm,string userId)
        {
            if (!ModelState.IsValid) View(ModelState);

            var client = await _clienteService.GetByIdentityId(userId);
            vm.clienteId = client.Id;    

            var confirnAccount = await _cuentaAhorroService.ConfirnAccount(vm.ProductDestinoIde);

            if(confirnAccount == false)
            {
                ModelState.AddModelError("No existe", "La cuenta a la que quiere transferir no existe");
                await CuentasPersonales(userId);

                return View(vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if(montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
                await CuentasPersonales(userId);

                return View(vm);
            }

            var clienteTransaccion = await _cuentaAhorroService.GetByIdentifier(vm.ProductDestinoIde);

            var datosClienteTransaccion = await _clienteService.GetById(clienteTransaccion.ClientId);
            vm.FirstName = datosClienteTransaccion.FirstName;
            vm.LastName = datosClienteTransaccion.LatsName;
         

            return RedirectToAction("PagoExpresoAction", vm);
        }

        public async Task<IActionResult> PagoExpresoAction(SaveTransaccionViewModel vm)
        {
             ViewBag.Nombre = vm.FirstName;
             ViewBag.apellido = vm.LastName;

             return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PagoExpresoActionPost(SaveTransaccionViewModel vm )
        {
            await AgregarTransaccion(vm);

            return View("PagoExpreso");
        }

        public async Task<IActionResult> PagoBeneficiarios(string userId)
        {

            await CuentasPersonales(userId);
            await CuentasBeneficiario(userId);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PagoBeneficiarios(SaveTransaccionViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                await CuentasPersonales(vm.userId);
                await CuentasBeneficiario(vm.userId);

                return View(vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if (montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
                await CuentasPersonales(vm.userId);
                await CuentasBeneficiario(vm.userId);

                return View(vm);
            }

            var cuenta = await _clienteService.GetByIdentityId(vm.userId);
            var beneficiario = await _beneficiarioService.GetBeneficiaryList(cuenta.Id);

            foreach(var item in beneficiario)
            {
                vm.FirstName = item.Name;
                vm.LastName = item.LastName;
            }

            return RedirectToAction("PagoBeneficiariosAction", vm);
        }

        public async Task<IActionResult> PagoBeneficiariosAction(SaveTransaccionViewModel vm)
        {
            ViewBag.Nombre = vm.FirstName;
            ViewBag.apellido = vm.LastName;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PagoBeneficiariosActionPost(SaveTransaccionViewModel vm)
        {
            await AgregarTransaccion(vm);   

            return RedirectToAction("Index", "Customer", new {userId = vm.userId });

        }
        public async Task<IActionResult> PagoCuentaCuenta(SaveTransaccionViewModel vm)
        {
            await CuentasPersonales(vm.userId);

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PagoCuentaCuentaPost(SaveTransaccionViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            if (vm.ProductDestinoIde == vm.ProductOrigenIde)
            {

                ModelState.AddModelError("Transferencia cuenta a cuenta", "No puedo realizar una transferencia a su propia Cuenta");
                await CuentasPersonales(vm.userId);
                return View("PagoCuentaCuenta", vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if (montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
                await CuentasPersonales(vm.userId);

                await CuentasPersonales(vm.userId);
                return View("PagoCuentaCuenta", vm);
            }

            var datosCuenta = await _clienteService.GetByIdentityId(vm.userId);

            vm.FirstName = datosCuenta.FirstName;
            vm.LastName = datosCuenta.LatsName;

            return RedirectToAction("PagoCuentaCuentaPostAction", vm);
        }

        public async Task<IActionResult> PagoCuentaCuentaPostAction(SaveTransaccionViewModel vm)
        {
            ViewBag.Nombre = vm.FirstName;
            ViewBag.apellido = vm.LastName;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> PagoCuentaCuentaPostActionPost(SaveTransaccionViewModel vm)
        {

            await AgregarTransaccion(vm);

            return RedirectToAction("Index", "Customer", new { userId = vm.userId });

        }
        private async Task CuentasPersonales(string userId)
        {
            var client = await _clienteService.GetByIdentityId(userId);

            var cuentas = await _cuentaAhorroService.GetListByClientId(client.Id);

            var cuentasIdentificador = new List<int>();

            foreach (var item in cuentas)
            {
                cuentasIdentificador.Add(item.Identifier);
            }

            ViewBag.indentificador = cuentasIdentificador;
        }
        private async Task CuentasBeneficiario(string userId)
        {
            var cuenta = await _clienteService.GetByIdentityId(userId);
            var beneficiario = await _beneficiarioService.GetBeneficiaryList(cuenta.Id);

            ViewBag.identifierBeneficiario = beneficiario;
        }

        private async Task AgregarTransaccion(SaveTransaccionViewModel vm)
        {
            var clienteAcutal = await _clienteService.GetByIdentityId(vm.userId);
            var cuentaActual = await _cuentaAhorroService.GetByClientId(clienteAcutal.Id);
            vm.clienteId = clienteAcutal.Id;

            cuentaActual.Balance = cuentaActual.Balance - vm.Amount;
            SaveCuentaDeAhorroViewModel saveClient = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuentaActual);

            await _cuentaAhorroService.Editar(saveClient, saveClient.Id);

            var cuentaATransferir = await _cuentaAhorroService.GetByIdentifier(vm.ProductDestinoIde);

            cuentaATransferir.Balance = cuentaATransferir.Balance + vm.Amount;
            SaveCuentaDeAhorroViewModel saveCuentaATransferir = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuentaATransferir);

            await _cuentaAhorroService.Editar(saveCuentaATransferir, saveCuentaATransferir.Id);

           /* var cuentaOrigen = await _cuentaAhorroService.GetByIdentifier(vm.CuentaOrigenId);
            var cuentaDestino = await _cuentaAhorroService.GetByIdentifier(vm.CuentaDestinoId);

            vm.CuentaOrigenId = cuentaOrigen.Id;
            vm.CuentaDestinoId = cuentaDestino.Id;*/

            await _transaccionService.AddAsync(vm);
            await CuentasPersonales(vm.userId);
        }
    }
}
