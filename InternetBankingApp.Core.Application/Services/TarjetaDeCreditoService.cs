using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class TarjetaDeCreditoService : GenericService<TarjetaDeCreditoViewModel, SaveTarjetaDeCreditoViewModel, TarjetaDeCredito>, ITarjetaDeCreditoService
    {
        private readonly ITarjetaDeCreditoRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public TarjetaDeCreditoService(ITarjetaDeCreditoRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper) : base(repository, mapper)
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

        public async Task<TarjetaDeCreditoViewModel> GetByIdentifier(int identifier)
        {
            return _mapper.Map<TarjetaDeCreditoViewModel>(await _repository.GetByIdentifierAsync(identifier));
        }
    }
}
