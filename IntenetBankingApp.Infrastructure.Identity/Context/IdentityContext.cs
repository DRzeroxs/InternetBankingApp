using InternetBankingApp.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Identity");
            modelBuilder.Entity<ApplicationUser>(entity =>
            entity.ToTable(name: "Users")
            );

            modelBuilder.Entity<IdentityRole>(entity =>
            entity.ToTable(name: "Roles")
            );

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            entity.ToTable(name: "UserRoles")
            );


            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
           entity.ToTable(name: "UserLogins")
           );
        }
    }
}
