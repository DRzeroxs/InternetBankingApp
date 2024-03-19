using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.Customer;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.Services;
using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.Products;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Application.ViewModels.Transaccion;
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
        private readonly ITarjetaDeCreditoService _tarjetaDeCreditoService; 
        private readonly IPrestamoService _prestamoService; 
        private readonly IDashBoardService _dashBoardService;
        private readonly IBeneficiarioService _beneficiarioService;
        private readonly ITransaccionService _transaccionService;
        private readonly IAgregarTransferencia _agregarTransferencia;
        private readonly IObtenerCuentas _obenerCuentas;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController(IUserService userService, IMapper mapper,
            ICuentaDeAhorroService cuentaAhorroService, IClienteService clienteService
            ,ITarjetaDeCreditoService tarjetaDeCreditoService, IPrestamoService prestamoService,
            IDashBoardService dashBoardService, IBeneficiarioService beneficiarioService
            ,ITransaccionService transaccionService, IHttpContextAccessor httpContextAccessor,
            IAgregarTransferencia agregarTransferencia, IObtenerCuentas obtenerCuentas)
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
            _httpContextAccessor = httpContextAccessor;
            _agregarTransferencia = agregarTransferencia;
            _obenerCuentas = obtenerCuentas;
            ///Hay que solucionar esto. 
          //  CurrentUser = httpContextAccessor.HttpContext.Session.get<AuthenticationResponse>("User");
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

        public async Task<IActionResult> PagoTarjetaCredito(string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);
            var tarjetas = await _tarjetaDeCreditoService.GetProductViewModelByClientId(cliente.Id);
            var products = await _dashBoardService.GetAllProductsByClientIdAsync(cliente.Id);
           
            ViewBag.creditos = tarjetas;
            ViewBag.products = products.cuentas;
            ViewBag.productsCredito = products.tarjetas;
            ViewBag.productsPrestamo = products.prestamos;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PagoTarjetaCredito(SaveTransaccionViewModel sv, string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);
            var tarjetas = await _tarjetaDeCreditoService.GetProductViewModelByClientId(cliente.Id);
            var products = await _dashBoardService.GetAllProductsByClientIdAsync(cliente.Id);

            ViewBag.creditos = tarjetas;
            ViewBag.products = products.cuentas;
            ViewBag.productsCredito = products.tarjetas;
            ViewBag.productsPrestamo = products.prestamos;

            if (!ModelState.IsValid)
            {
                return View(sv);
            }

            var cuenta =   await _transaccionService.PagarTarjetaCredito(sv);


            if (sv.HasError)
            {
                sv.HasError = cuenta.HasError;
                sv.Error = cuenta.Error;
                return View(sv);
            }

            return RedirectToAction("Index", "Customer", await _userService.GetByIdAsync(userId));
        }
        public async Task<IActionResult> PagoExpreso(string userId)
        {
            ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(userId);

            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> PagoExpreso(SaveTransaccionViewModel vm,string userId)
        {
            if (!ModelState.IsValid) View(ModelState);

            var client = await _clienteService.GetByIdentityId(userId);
            vm.ClienteId = client.Id;    

            var confirnAccount = await _cuentaAhorroService.ConfirnAccount(vm.ProductDestinoIde);

            if(confirnAccount == false)
            {
                ModelState.AddModelError("No existe", "La cuenta a la que quiere transferir no existe");
             
                await _obenerCuentas.CuentasPersonales(userId);

                return View(vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if(montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
           
                await _obenerCuentas.CuentasPersonales(userId);

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
            await _agregarTransferencia.AgregarTransaccion(vm);

            return RedirectToAction("Index", new {userId = vm.userId });
        }

        public async Task<IActionResult> PagoBeneficiarios(string userId)
        {

            ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(userId);
       
            ViewBag.identifierBeneficiario = await _obenerCuentas.CuentasBeneficiario(userId);   

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PagoBeneficiarios(SaveTransaccionViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(vm.userId);
                ViewBag.identifierBeneficiario = await _obenerCuentas.CuentasBeneficiario(vm.userId);

                return View(vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if (montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
          
                ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(vm.userId);
                ViewBag.identifierBeneficiario = await _obenerCuentas.CuentasBeneficiario(vm.userId);

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
          
            await _agregarTransferencia.AgregarTransaccion(vm);

            return RedirectToAction("Index", "Customer", new {userId = vm.userId });

        }

        public async Task<IActionResult> PagoPrestamo(string userId)
        {
            var cliente = await _clienteService.GetByIdentityId(userId);

            if(cliente == null) return RedirectToAction("Index", "Customer", userId);

            await GetProductosParaPagoPrestamo(cliente.Id);

           // ViewBag.prestamos
           // ViewBag.cuentas

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PagoPrestamo(SaveTransaccionViewModel vm)
        {

            var cliente = await _clienteService.GetByIdentityId(vm.userId);

            if (!ModelState.IsValid)
            {
                await GetProductosParaPagoPrestamo(cliente.Id);
                return View(vm);
            }

            vm.ClienteId = cliente.Id;
            CuentaDeAhorroViewModel cuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);
            PrestamoViewModel prestamo = await _prestamoService.GetByIdentifier(vm.ProductDestinoIde);

            if(cuenta == null || prestamo == null)
            {
                await GetProductosParaPagoPrestamo(cliente.Id);
                //Agregar un error de "Cuenta no encontrada" en el Model para que se vea en la vista
                return View(vm);
            }

            if(cuenta.Balance >= vm.Amount)
            {
                if(prestamo.CurrentDebt < vm.Amount)
                {
                    vm.Amount = prestamo.CurrentDebt;
                }

                prestamo.CurrentDebt -= vm.Amount;
                cuenta.Balance -= vm.Amount;

            }
            else
            {
                await GetProductosParaPagoPrestamo(cliente.Id);
                //Agregar un error de "Balance insuficiente" en el Model para que se vea en la vista
                return View(vm);
            }

            //Realizar transsacion
            await _transaccionService.AddAsync(vm);


            SavePrestamoViewModel prestamoUpdate = new SavePrestamoViewModel
            {

                Id = prestamo.Id,
                InitialDebt = prestamo.InitialDebt,
                Identifier = prestamo.Identifier,
                CurrentDebt = prestamo.CurrentDebt,
                ClienteId = prestamo.ClienteId

            };

            SaveCuentaDeAhorroViewModel cuentaUpdate = new SaveCuentaDeAhorroViewModel
            {

                Id = cuenta.Id,
                Balance = cuenta.Balance,
                Main = cuenta.Main,
                Identifier = cuenta.Identifier,
                ClientId = cuenta.ClientId

            };

            await _prestamoService.Editar(prestamoUpdate, prestamoUpdate.Id);
            await _cuentaAhorroService.Editar(cuentaUpdate, cuentaUpdate.Id);

            return RedirectToAction("Index", "Customer", await _userService.GetAllUser());
        }
        public async Task<IActionResult> PagoCuentaCuenta(string userId)
        {
           
            ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(userId);
            SaveTransaccionViewModel vm = new();

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> PagoCuentaCuentaPost(SaveTransaccionViewModel vm)
        {
            if (!ModelState.IsValid) return View("PagoCuentaCuenta", new {userId = vm.userId });

            if (vm.ProductDestinoIde == vm.ProductOrigenIde)
            {

                ModelState.AddModelError("Transferencia cuenta a cuenta", "No puedo realizar una transferencia a su propia Cuenta");
               
                ViewBag.indentificador = await _obenerCuentas.CuentasPersonales(vm.userId);
                return View("PagoCuentaCuenta", vm);
            }

            var montoCuenta = await _cuentaAhorroService.GetByIdentifier(vm.ProductOrigenIde);

            if (montoCuenta.Balance < vm.Amount)
            {
                ModelState.AddModelError("No tiene saldo", "No cuenta con saldo suficiente para realizar la transaccion");
              

                await _obenerCuentas.CuentasPersonales(vm.userId);
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

            await  _agregarTransferencia.AgregarTransaccion(vm);

            return RedirectToAction("Index", "Customer", new { userId = vm.userId });

        }  
        private async Task GetProductosParaPagoPrestamo(int clienteId)
        {
            var prestamos = await _prestamoService.GetProductViewModelByClientId(clienteId);
            var cuentas = await _cuentaAhorroService.GetProductViewModelByClientId(clienteId);

            ViewBag.prestamos = prestamos;
            ViewBag.cuentas = cuentas;
        }
       
        
    }
}
