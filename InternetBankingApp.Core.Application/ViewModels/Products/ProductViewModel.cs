using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public List<CuentaDeAhorroViewModel> cuentas {  get; set; }
        public List<TarjetaDeCreditoViewModel> tarjetas { get; set; }
        public List<PrestamoViewModel> prestamos { get; set; }
    }
}
