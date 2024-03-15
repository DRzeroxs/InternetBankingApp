using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito
{
    public class SaveTarjetaDeCreditoViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Digite un limite para la tarjeta")]
        [Range(1000, int.MaxValue, ErrorMessage = "El limite minimo es 1000")]
        public double Limit { get; set; }
        public double Debt { get; set; } = 0;

        public int Identifier { get; set; }

        [Required(ErrorMessage ="Seleccione un Cliente")]
        [Range(1, int.MaxValue, ErrorMessage ="Seleccione un Cliente Valido")]
        public int ClienteId { get; set; }
    }
}
