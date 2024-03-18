using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Enums
{
    public enum ETipoPago
    {
        None = 0,
        PagoExpreo = 1,
        PagoTarjeta = 2,
        PagoPretamo = 3,
        PagoBeneficiario =4,
    }
}
