using InternetBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class Beneficiario : AuditableBaseEntity
    {
        public string Name { get; set; }

        //Laves foraneas
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public int CuentaIdentifier { get; set; }

        //Conductores
        public Cliente Cliente { get; set; }
    }
}
