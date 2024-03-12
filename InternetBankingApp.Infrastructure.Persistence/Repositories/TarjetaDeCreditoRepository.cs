using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Domain.Entities;
using InternetBankingApp.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Infrastructure.Persistence.Repositories
{
    public class TarjetaDeCreditoRepository : GenericRepository<TarjetaDeCredito>, ITarjetaDeCreditoRepository
    {
        private readonly ApplicationContext _context;

        public TarjetaDeCreditoRepository (ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
