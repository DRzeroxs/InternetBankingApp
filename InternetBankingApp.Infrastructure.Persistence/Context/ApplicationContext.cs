using InternetBankingApp.Core.Domain.Common;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Beneficiario> Beneficiarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<CuentaDeAhorro> CuentasDeAhorro { get; set; }
        public DbSet<Prestamo> Prestamos { get; set; }
        public DbSet<TarjetaDeCredito> TarjetasDeCredito { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.Now;
                        entry.Entity.LastModifiedby = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
