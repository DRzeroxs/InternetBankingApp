using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IRepository
{
    public interface IGetInfoProductsRepository<Entity> where Entity : class
    {
        Task<Entity> GetByIdentifierAsync(int identifier);
        Task<List<int>> GetAllIdentifiersAsync();
        Task<List<Entity>> GetProductByClientIdAsync(int clienteId);
    }
}
