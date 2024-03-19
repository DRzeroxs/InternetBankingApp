using AutoMapper;
using InternetBankingApp.Core.Application.Interfaces.Customer;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.Services;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Transaccion;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Helpers
{
    public class AgregarTransferencia : IAgregarTransferencia
    {
        private readonly IClienteService _clienteService;
        private readonly ICuentaDeAhorroService _cuentaDeAhorroService;
        private readonly IMapper _mapper;
        private readonly ITransaccionService _transaccionService;
        public AgregarTransferencia(IClienteService clienteService, ICuentaDeAhorroService cuentaDeAhorroService,
            IMapper mapper, ITransaccionService transaccionService)
        {
            _clienteService = clienteService;
            _cuentaDeAhorroService = cuentaDeAhorroService;
            _mapper = mapper;
            _transaccionService = transaccionService;
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

            await _transaccionService.AddAsync(vm);
            // await CuentasPersonales(vm.userId);
        }
    }
}
