using InternetBankingApp.Core.Application.ViewModels.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Prestamo
{
    public class PrestamoViewModel
    {
        public int Id { get; set; }
        public double InitialDebt { get; set; }
        public double CurrentDebt { get; set; }

        public int Identifier { get; set; }
        public int ClienteId { get; set; }
        public ClienteViewModel? Cliente { get; set; }
    }
}
