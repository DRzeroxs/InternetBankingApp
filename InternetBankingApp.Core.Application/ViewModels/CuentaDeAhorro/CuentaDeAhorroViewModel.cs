using InternetBankingApp.Core.Application.ViewModels.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro
{
    public class CuentaDeAhorroViewModel
    {
        public virtual int Id { get; set; }
        public double Balance { get; set; }
        public bool Main { get; set; }
        public int Identifier { get; set; }
        public int ClientId { get; set; }
        public ClienteViewModel Cliente { get; set; }
    }
}
