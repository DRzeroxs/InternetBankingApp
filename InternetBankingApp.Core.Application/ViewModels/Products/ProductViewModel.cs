using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.ViewModels.Products
{
    public class ProductViewModel
    {
        public int Identifier { get; set; }
        public int ClientId { get; set; }

        //Datos para Cuenta
        public double Balance { get; set; }

        //Datos para Tarjeta
        public double Limit { get; set; }
        public double Debt {  get; set; }

        //Datos para prestamo
        public double InitialDebt { get; set; }
        public double CurrentDebt { get; set; }


        public DateTime Date { get; set; }
    }
}
