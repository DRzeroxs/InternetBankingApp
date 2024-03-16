using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Domain.Entities;
using InternetBankingApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<TarjetaDeCredito> GetByIdentifierAsync(int identifier)
        {
            var entity = await _context.Set<TarjetaDeCredito>()
                .FirstOrDefaultAsync(c => c.Identifier == identifier);

            return entity;
        }

        public async Task<List<int>> GetAllIdentifiersAsync()
        {
            List<int> result = await _context.Set<TarjetaDeCredito>().
                Select(c => c.Identifier)
                .ToListAsync();

            return result;
        }
    }
}
