using InternetBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class CuentaDeAhorro : AuditableBaseEntity
    {
        public double Balance {  get; set; }

        public bool Main {  get; set; }

        //Llave foranea
        [ForeignKey("Cliente")]
        public int ClientId { get; set; }

        //conductores
        public Cliente Cliente { get; set; }

    }
}
