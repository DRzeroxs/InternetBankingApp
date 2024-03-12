using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
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
        public int CuentaOrigenId { get; set; }
        public int CuentaDestinoId { get; set; }

        //visores
        public ClienteViewModel? Cliente { get; set; }
        public CuentaDeAhorroViewModel? CuentaDeOrigen { get; set; }
        public CuentaDeAhorroViewModel?CuentaDeDestino { get; set; }
    }
}
