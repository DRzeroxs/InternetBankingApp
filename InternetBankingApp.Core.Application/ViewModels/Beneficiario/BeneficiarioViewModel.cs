using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Beneficiario
{
    public class BeneficiarioViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClienteId { get; set; }
        public int CuentaDeAhorroId { get; set; }

        public ClienteViewModel? Cliente { get; set; }
    }
}
