using InternetBankingApp.Core.Application.Interfaces.Customer;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.Services;
using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Helpers
{
    public class ObtenerCuentas : IObtenerCuentas
    {
        private readonly IClienteService _clienteService;
        private readonly IBeneficiarioService _beneficiarioService;
        private readonly ICuentaDeAhorroService _cuentaAhorroService;

        public ObtenerCuentas(IClienteService clienteService, IBeneficiarioService beneficiarioService, ICuentaDeAhorroService cuentaDeAhorro)
        {
            _clienteService = clienteService;
            _beneficiarioService = beneficiarioService;
            _cuentaAhorroService = cuentaDeAhorro;
        }

        public async Task<List<ClientBeneficiaryViewModel>> CuentasBeneficiario(string userId)
        {
            var cuenta = await _clienteService.GetByIdentityId(userId);
            var beneficiario = await _beneficiarioService.GetBeneficiaryList(cuenta.Id);
            return beneficiario;
        }

        public async Task<List<int>> CuentasPersonales(string userId)
        {
            var client = await _clienteService.GetByIdentityId(userId);

            var cuentas = await _cuentaAhorroService.GetListByClientId(client.Id);

            var cuentasIdentificador = new List<int>();

            foreach (var item in cuentas)
            {
                cuentasIdentificador.Add(item.Identifier);
            }

            return cuentasIdentificador;
        }

        public async Task<List<ClientBeneficiaryViewModel>> ObtenerDatosBeneficiarios(string userId)
        {
            var clientId = await _clienteService.GetByIdentityId(userId);
            var benficiarios = await _beneficiarioService.GetBeneficiaryList(clientId.Id);

            return benficiarios.ToList();
        }
    }
}
