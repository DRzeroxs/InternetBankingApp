using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Transaccion
{
    public class SaveTransaccionViewModel
    {
        public int? Id { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? userId { get; set; }

        [Required(ErrorMessage ="Seleccione un metodo")]
        [Range(1, 6, ErrorMessage ="Seleccione un metodo valido")]
        public int Tipe {  get; set; }

        [Required(ErrorMessage = "Ingrese una cantidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una cantidad valida")]
        public double Amount { get; set; }

        public DateTime? Date { get; set; } = DateTime.Now;
      
        public int ClienteId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cuenta de origen es requerida")]

        public int ProductOrigenIde { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "La cuenta de destino es requerida")]
        public int ProductDestinoIde { get; set; }

        //Visores
        public ClienteViewModel? Cliente { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
