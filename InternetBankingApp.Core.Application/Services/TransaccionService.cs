using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Application.ViewModels.Transaccion;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class TransaccionService : GenericService<TransaccionViewModel, SaveTransaccionViewModel, Transaccion>, ITransaccionService
    {
        private readonly ITransaccionRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;
        private readonly ITarjetaDeCreditoService _tarjetaDeCreditoService;
        private readonly IClienteService _clienteService;
        private readonly IDashBoardService _dashBoardService;
        private readonly ICuentaDeAhorroService _cuentaDeAhorroService;
        private readonly IPrestamoService _prestamoService;

        public TransaccionService(ITransaccionRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper, ITarjetaDeCreditoService tarjetaDeCreditoService, IClienteService clienteService, IDashBoardService dashBoardService, ICuentaDeAhorroService cuentaDeAhorroService, IPrestamoService prestamoService) : base(repository, mapper)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("User");
            _mapper = mapper;
            _tarjetaDeCreditoService = tarjetaDeCreditoService;
            _clienteService = clienteService;
            _dashBoardService = dashBoardService;
            _cuentaDeAhorroService = cuentaDeAhorroService;
            _prestamoService = prestamoService;
        }

        public  async Task<SaveTransaccionViewModel> PagarTarjetaCredito(SaveTransaccionViewModel sv)
        {
            var userId = userViewModel.Id;

            var cliente = await _clienteService.GetByIdentityId(userId);
            sv.ClienteId = cliente.Id;
            var tarjetas = await _tarjetaDeCreditoService.GetProductViewModelByClientId(cliente.Id);
            var cuentas = await _dashBoardService.GetAllProductsByClientIdAsync(cliente.Id);
            var tarjetaSelecionada = tarjetas.FirstOrDefault(x => x.Id == sv.ProductDestinoIde);

            #region "Cuenta Ahorro"

            if (cuentas.cuentas != null)
            {
                var cuentaSelecionada = cuentas.cuentas.FirstOrDefault(x => x.Id == sv.ProductOrigenIde);
                if (cuentaSelecionada != null)
                {
                    if (cuentaSelecionada.Balance >= sv.Amount)
                    {
                        var monto = Math.Min(sv.Amount, tarjetaSelecionada.Debt);
                        tarjetaSelecionada.Debt -= monto;
                        cuentaSelecionada.Balance -= monto;
                    }
                    else
                    {
                        sv.HasError = true;
                        sv.Error = "Su cuenta de ahorro no tiene suficiente balance.";
                    }

                    sv.ProductOrigenIde = cuentaSelecionada.Identifier;
                    sv.ProductDestinoIde = tarjetaSelecionada.Identifier;

                    var tarjeta = _mapper.Map<SaveTarjetaDeCreditoViewModel>(tarjetaSelecionada);
                    var cuenta = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuentaSelecionada);
                    await _tarjetaDeCreditoService.Editar(tarjeta, tarjeta.Id);
                    await _cuentaDeAhorroService.Editar(cuenta, cuentaSelecionada.Id);
                }

            }
            #endregion

            #region "Cuenta Prestamos"
            if (cuentas.prestamos != null)
            {
                var cuentaSelecionada = cuentas.prestamos.FirstOrDefault(x => x.Id == sv.ProductOrigenIde);
                if (cuentaSelecionada != null)
                {
                    if (cuentaSelecionada.InitialDebt >= sv.Amount)
                    {
                        var monto = Math.Min(sv.Amount, tarjetaSelecionada.Debt);
                        tarjetaSelecionada.Debt -= monto;
                        cuentaSelecionada.CurrentDebt += monto;
                    }
                    else
                    {
                        sv.HasError = true;
                        sv.Error = "Su cuenta de prestamo no tiene suficiente balance.";
                    }

                    sv.ProductOrigenIde = cuentaSelecionada.Identifier;
                    sv.ProductDestinoIde = tarjetaSelecionada.Identifier;

                    var tarjeta = _mapper.Map<SaveTarjetaDeCreditoViewModel>(tarjetaSelecionada);
                    var cuenta = _mapper.Map<SavePrestamoViewModel>(cuentaSelecionada);

                    await _tarjetaDeCreditoService.Editar(tarjeta, tarjeta.Id);
                    await _prestamoService.Editar(cuenta, cuentaSelecionada.Id);
                }
            }
            #endregion

            #region "Cuenta Credito"

            if (cuentas.tarjetas != null)
            {
                var cuentaSelecionada = cuentas.tarjetas.FirstOrDefault(x => x.Id == sv.ProductOrigenIde);
                if(cuentaSelecionada != null)
                {
                    if (cuentaSelecionada.Limit >= sv.Amount)
                    {
                        var monto = Math.Min(sv.Amount, tarjetaSelecionada.Debt);
                        tarjetaSelecionada.Debt -= monto;
                        cuentaSelecionada.Debt += monto;
                        cuentaSelecionada.Limit -= cuentaSelecionada.Debt;
                    }
                    else
                    {
                        sv.HasError = true;
                        sv.Error = "Su tarjeta de credito no tiene  suficiente balance.";
                    }

                    sv.ProductOrigenIde = cuentaSelecionada.Identifier;
                    sv.ProductDestinoIde = tarjetaSelecionada.Identifier;

                    var tarjeta = _mapper.Map<SaveTarjetaDeCreditoViewModel>(tarjetaSelecionada);
                    var cuenta = _mapper.Map<SaveTarjetaDeCreditoViewModel>(cuentaSelecionada);


                    await _tarjetaDeCreditoService.Editar(tarjeta, tarjeta.Id);
                    await _tarjetaDeCreditoService.Editar(cuenta, cuenta.Id);
                }
              
            }

            #endregion
            sv.Tipe = (int)ETipoPago.PagoTarjeta;
            var saveTransation = _mapper.Map<Transaccion>(sv);

            await _repository.AddAsync(saveTransation);
            return sv;
        }
        public async Task AgregarTransaccion(SaveTransaccionViewModel vm)
        {
            var clienteAcutal = await _clienteService.GetByIdentityId(vm.userId);
            var cuentaActual = await _cuentaDeAhorroService.GetByClientId(clienteAcutal.Id);
            vm.ClienteId = clienteAcutal.Id;

            cuentaActual.Balance = cuentaActual.Balance - vm.Amount;
            SaveCuentaDeAhorroViewModel saveClient = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuentaActual);

            await _cuentaDeAhorroService.Editar(saveClient, saveClient.Id);

            var cuentaATransferir = await _cuentaDeAhorroService.GetByIdentifier(vm.ProductDestinoIde);

            cuentaATransferir.Balance = cuentaATransferir.Balance + vm.Amount;
            SaveCuentaDeAhorroViewModel saveCuentaATransferir = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuentaATransferir);

            await _cuentaDeAhorroService.Editar(saveCuentaATransferir, saveCuentaATransferir.Id);

            Transaccion SaveVm = _mapper.Map<Transaccion>(vm);

            await _repository.AddAsync(SaveVm);
           
        }

       

    }
}
