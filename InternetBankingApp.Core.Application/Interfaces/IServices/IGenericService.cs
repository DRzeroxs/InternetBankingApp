using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface IGenericService <ViewModel, saveViewModel, Entity> 
        where ViewModel : class 
        where saveViewModel : class
        where Entity : class
    {
        Task<saveViewModel> AddAsync(saveViewModel vm);
        Task Editar(saveViewModel vm, int ID);
        Task Eliminar(int Id);
        Task<List<ViewModel>> GetAllAsync();
        Task<saveViewModel> GetById(int Id);
    }
}
