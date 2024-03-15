using InternetBankingApp.Core.Application.ViewModels.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito
{
    public class TarjetaDeCreditoViewModel
    {
        public int Id {  get; set; }
        public double Limit { get; set; }
        public double Debt {  get; set; }

        public int Identifier { get; set; }
        public int ClienteId { get; set; }
        public ClienteViewModel? Cliente { get; set; }
    }
}
