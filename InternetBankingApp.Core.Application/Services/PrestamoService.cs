using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class PrestamoService : GenericService<PrestamoViewModel, SavePrestamoViewModel, Prestamo>, IPrestamoService
    {
        private readonly IPrestamoRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public PrestamoService(IPrestamoRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("user");
            _mapper = mapper;
        }

        public async Task<List<int>> GetAllIdentifiers()
        {
            return await _repository.GetAllIdentifiersAsync();
        }

        public async Task<PrestamoViewModel> GetByIdentifier(int identifier)
        {
            return _mapper.Map<PrestamoViewModel>(await _repository.GetByIdentifierAsync(identifier));
        }

        public async Task<List<PrestamoViewModel>> GetProductViewModelByClientId(int clienteId)
        {
            var list = await _repository.GetProductByUserIdAsync(clienteId);

            return _mapper.Map<List<PrestamoViewModel>>(list);
        }
    }
}
