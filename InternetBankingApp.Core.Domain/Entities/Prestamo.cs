using InternetBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class Prestamo : AuditableBaseEntity
    {
        public double InitialDebt { get; set; }
        public double CurrentDebt {  get; set; }

        public int Identifier { get; set; }

        //Llaves foraneas
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        //Conductor
        public Cliente Cliente { get; set;}
    }
}
