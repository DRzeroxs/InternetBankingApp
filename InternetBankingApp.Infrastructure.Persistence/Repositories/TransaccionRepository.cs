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
    public class TransaccionRepository : GenericRepository<Transaccion> , ITransaccionRepository
    {
        private readonly ApplicationContext _context;
        
        public TransaccionRepository (ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CuentaDeTransacciones()
        {
            var TrasanccionesList = await _context.Transacciones.ToListAsync();

            var count =  TrasanccionesList.Count();

            return count;
        }
        public async Task<int> CuentraTransaccionesHoy()
        {
            var TransaccionesList = await _context.Transacciones.ToListAsync();
          
            var count = TransaccionesList.Where(t => t.Date.Date == DateTime.Today).Count();  

            return count;                       
        }

        public async Task<int> CuentasDePago()
        {
            var TransaccionesList = await _context.Transacciones.ToListAsync();

            var PagosList = TransaccionesList.Where(t => t.Tipe <= 4);

            var count = PagosList.Count();

            return count;
        }
        public async Task<int> CuentasDePagoHoy()
        {
            var TransaccionesList = await _context.Transacciones.ToListAsync();

            var PagosList = TransaccionesList.Where(t => t.Tipe <= 4).Where(p => p.Date.Date == DateTime.Today).Count();

            var count = PagosList;

            return count;
        }
    }
}
