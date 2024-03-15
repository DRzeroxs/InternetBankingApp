using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Prestamo
{
    public class SavePrestamoViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese una cantidad para el prestamo")]
        [Range(100, int.MaxValue, ErrorMessage ="La cantidad minima es 100")]
        public double InitialDebt { get; set; }
        public int Identifier { get; set; }
        public double CurrentDebt { get; set; }
        public int ClienteId { get; set; }
    }
}
