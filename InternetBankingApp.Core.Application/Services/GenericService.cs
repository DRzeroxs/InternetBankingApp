using AutoMapper;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class GenericService<ViewModel, postViewModel, Entity> : IGenericService<ViewModel, postViewModel, Entity>
      where ViewModel : class
      where postViewModel : class
      where Entity : class
    {
        private IGenericRepository<Entity> _repository;
        private readonly IMapper _mapper;

        public GenericService(IGenericRepository<Entity> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<postViewModel> AddAsync(postViewModel vm)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            entity = await _repository.AddAsync(entity);

            postViewModel postVm = _mapper.Map<postViewModel>(entity);

            return postVm;
        }

        public virtual async Task Editar(postViewModel vm, int ID)
        {
            Entity entity = _mapper.Map<Entity>(vm);

            await _repository.UpdateAsync(entity, ID);
        }

        public async Task Eliminar(int Id)
        {
            Entity entity = await _repository.GetById(Id);

            await _repository.DeleteAsync(entity);
        }

        public virtual async Task<List<ViewModel>> GetAllAsync()
        {
            var Lista = await _repository.GetAll();

            return _mapper.Map<List<ViewModel>>(Lista);
        }

        public async Task<postViewModel> GetById(int Id)
        {
            Entity entity = await _repository.GetById(Id);

            postViewModel postVm = _mapper.Map<postViewModel>(entity);

            
            return postVm;
        }
    }
}
