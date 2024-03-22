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
    public class CuentaDeAhorroRepository : GenericRepository<CuentaDeAhorro>, ICuentaDeAhorroRepository
    {
        private readonly ApplicationContext _context;

        public CuentaDeAhorroRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CuentaDeAhorro> GetByIdentifierAsync(int identifier)
        {
            var entity = await _context.Set<CuentaDeAhorro>()
                .FirstOrDefaultAsync(c => c.Identifier == identifier);

            return entity;
        }

        public async Task<CuentaDeAhorro> GetMainByClientIdAsync(int clientId)
        {
            var entity = await _context.Set<CuentaDeAhorro>()
                .FirstOrDefaultAsync(c => c.ClientId == clientId && c.Main);

            return entity;
        }

        public async Task<List<int>> GetAllIdentifiersAsync()
        {
            List<int> result = await _context.Set<CuentaDeAhorro>().
                Select(c => c.Identifier)
                .ToListAsync();

            return result;
        }

        public async Task<List<CuentaDeAhorro>> GetProductByClientIdAsync(int clienteId)
        {
            return await _context.Set<CuentaDeAhorro>().Where(c => c.ClientId == clienteId).ToListAsync();
        }
    }
}
