using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro
{
    public class SaveCuentaDeAhorroViewModel
    {
        public  int Id { get; set; }

        [Required(ErrorMessage = "Ingrese un monto minimo")]
        [Range(100, int.MaxValue, ErrorMessage ="Ingrese un monto minimo valido (min 100)")]
        public double Balance { get; set; }
        public bool Main { get; set; } = false;
        public int Identifier { get; set; } 

        [Required(ErrorMessage = "Seleccione un monto minimo")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione un monto Cliente valido")]
        public int ClientId { get; set; }
    }
}
