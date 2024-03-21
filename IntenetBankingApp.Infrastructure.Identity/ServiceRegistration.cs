
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Infrastructure.Identity.Context;
using InternetBankingApp.Infrastructure.Identity.Entities;
using InternetBankingApp.Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
                services.AddDbContext<IdentityContext>(options =>
                {
                    options.EnableSensitiveDataLogging();
                    options.UseSqlServer(configuration.GetConnectionString("Identityconexion"),
                    m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName));
                });

          
            services.AddIdentity<ApplicationUser, IdentityRole>()
             .AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/User/AccessDenied";

            });

            services.AddAuthentication();

            #region"Service"
            services.AddTransient<IAccountServices, AccountServices>();
            #endregion

        }
    }
}
