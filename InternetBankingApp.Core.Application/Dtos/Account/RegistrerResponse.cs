using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Dtos.Account
{
    public class RegistrerResponse
    {
        public bool HasError { get; set; }
        public string Error { get; set; }
    }
}
