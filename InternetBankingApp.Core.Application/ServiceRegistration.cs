using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.Customer;
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region "Services"
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<ICuentaDeAhorroService, CuentaDeAhorroService>();
            services.AddTransient<ITarjetaDeCreditoService, TarjetaDeCreditoService>();
            services.AddTransient<IPrestamoService, PrestamoService>();
            services.AddTransient<IDashBoardService, DashBoardService>();
            services.AddTransient<IBeneficiarioService, BeneficiarioService>();
            services.AddTransient<ITransaccionService, TransaccionService>();  
            services.AddTransient<IObtenerCuentas, ObtenerCuentas>();
            #endregion
        }
    }
}
