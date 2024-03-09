using InternetBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class TarjetaDeCredito : AuditableBaseEntity
    {
        public double Limit { get; set; }
        public double Debt {  get; set; }

        //Llave foranea
        [ForeignKey("Cliente")] 
        public int ClienteId { get; set; }

        //conductor
        public Cliente Cliente { get; set; }
    }
}
