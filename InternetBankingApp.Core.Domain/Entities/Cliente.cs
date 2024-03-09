using InternetBankingApp.Core.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetBankingApp.Core.Domain.Entities
{
    public class Cliente : AuditableBaseEntity
    {
        public string UserID { get; set; }

        //conductores
        [InverseProperty("Cliente")]
        public ICollection<Transaccion> Transacciones { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<TarjetaDeCredito> TarjetasDeCredito { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<Prestamo> Prestamo { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<CuentaDeAhorro> CuentasDeAhorro { get; set; }

        [InverseProperty("Cliente")]
        public ICollection<Beneficiario> Beneficiarios { get; set; }
    }
}
