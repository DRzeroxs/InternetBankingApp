using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Beneficiario
{
    public class SaveBeneficiarioViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre para el beneficiario")]
        public string Name { get; set; }

        public int ClienteId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Ingrese una cuenta de ahorro valida")]
        public int CuentaDeAhorroId { get; set; }
    }
}
