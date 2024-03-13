using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Infrastructure.Persistence.Context;
using InternetBankingApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {

            #region Context
            services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(
                        configuration.GetConnectionString("conexion"),
                            m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)
                    )
                );
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IBeneficiarioRepository, BeneficiarioRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<ICuentaDeAhorroRepository, CuentaDeAhorroRepository>();
            services.AddTransient<IPrestamoRepository, PrestamoRepository>();
            services.AddTransient<ITarjetaDeCreditoRepository, TarjetaDeCreditoRepository>();
            services.AddTransient<ITransaccionRepository, TransaccionRepository>();
            #endregion

        }
    }
}
