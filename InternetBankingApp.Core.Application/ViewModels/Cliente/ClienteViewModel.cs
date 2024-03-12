using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Cliente
{
    public class ClienteViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LatsName { get; set; }

        //Agregar los viewmodel de tarjeta, cuentas de ahorro, beneficiario y prestamop
        public List<BeneficiarioViewModel>? Beneficiarios { get; set; }
        public List<CuentaDeAhorroViewModel>? Cuentas { get; set; }
        public List<PrestamoViewModel>? Prestamos { get; set; }
        public List<TarjetaDeCreditoViewModel>? Tarjetas {  get; set; }
    }
}
