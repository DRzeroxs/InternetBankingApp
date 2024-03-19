using InternetBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class Transaccion : AuditableBaseEntity
    {
        public int Tipe { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        // Llaves foraneas
        [ForeignKey("Cliente")]
        public int clienteId { get; set; }

        [ForeignKey("CuentaOrigen")]
        public int ProductOrigenIde { get; set; }
        public int ProductDestinoIde { get; set; }

        // Conductores
        public Cliente Cliente { get; set; }
    }
}
