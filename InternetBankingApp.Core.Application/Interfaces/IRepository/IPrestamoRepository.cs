using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IRepository
{
    public interface IPrestamoRepository : IGenericRepository<Prestamo>, IGetInfoProductsRepository<Prestamo>
    {
    }
}
