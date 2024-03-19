using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Transaccion
{
    public class TransaccionViewModel
    {
        public int Id { get; set; }
        public int Tipe {  get; set; }
        public DateTime Date { get; set; }
        public int ClienteId { get; set; }
        public int ProductOrigenIde { get; set; }
        public int ProductDestinoIde { get; set; }

        //visores
        public ClienteViewModel? Cliente { get; set; }
        public CuentaDeAhorroViewModel? CuentaAhorro { get; set; }
        public TarjetaDeCreditoViewModel? TarjetaDeCredito { get; set; }
        public PrestamoViewModel? Prestamo { get; set; }
    }
}
