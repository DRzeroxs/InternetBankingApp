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
    public class PrestamoRepository : GenericRepository<Prestamo> , IPrestamoRepository
    {
        private readonly ApplicationContext _context;

        public PrestamoRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<int>> GetAllIdentifiersAsync()
        {
            List<int> result = await _context.Set<Prestamo>().
                Select(c => c.Identifier)
                .ToListAsync();

            return result;
        }

        public async Task<Prestamo> GetByIdentifierAsync(int identifier)
        {
            var entity = await _context.Set<Prestamo>()
                .FirstOrDefaultAsync(c => c.Identifier == identifier);

            return entity;
        }

        public async Task<List<Prestamo>> GetProductByUserIdAsync(int clienteId)
        {
            return await _context.Set<Prestamo>().Where(p => p.ClienteId == clienteId).ToListAsync();
        }
    }
}
