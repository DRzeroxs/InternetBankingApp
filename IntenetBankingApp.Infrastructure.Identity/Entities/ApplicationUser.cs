using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LatsName { get; set; }
        public string TypeOfUser { get; set; }  
        public bool IsActive { get; set; }
        public string IdentificationCard { get; set; }
    }
}
