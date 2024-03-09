using InternetBankingApp.Core.Application.Enums;
using InternetBankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Identity.Seeds
{
    public static class DefaultCustomerUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultUser = new();

            defaultUser.FirstName = "Antonio";

            defaultUser.LatsName = "Lorenzo";

            defaultUser.Email = "Juan@gmail.com";

            defaultUser.EmailConfirmed = true;

            defaultUser.UserName = "Cleimer";

            defaultUser.PhoneNumberConfirmed = true;

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = userManager.FindByEmailAsync(defaultUser.Email);

                if (user != null)
                {
                    await userManager.CreateAsync(defaultUser, "123Pa$$work");
                    await userManager.AddToRoleAsync(defaultUser, Roles.Customer.ToString());

                }
            }
        }
    }
}
