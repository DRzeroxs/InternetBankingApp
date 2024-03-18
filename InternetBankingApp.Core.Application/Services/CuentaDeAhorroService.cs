using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
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
    public class CuentaDeAhorroService : GenericService<CuentaDeAhorroViewModel, SaveCuentaDeAhorroViewModel, CuentaDeAhorro>, ICuentaDeAhorroService
    {
        private readonly ICuentaDeAhorroRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public CuentaDeAhorroService(ICuentaDeAhorroRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            userViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("user");
            _mapper = mapper;
        }
        public async Task<bool> ConfirnAccount(int identifier)
        {
            var cuentasList = await _repository.GetAll();
            var cuenta = cuentasList.FirstOrDefault(c => c.Identifier == identifier);

            if (cuenta == null) return false;
            else return true;
           
        }
        public async Task<List<CuentaDeAhorroViewModel>> GetListByClientId(int ClientId)
        {
            var cuentaList = await _repository.GetAll();

            var cuentas = from c in cuentaList
                            where c.ClientId == ClientId
                            select new CuentaDeAhorroViewModel
                            {
                               Id = c.ClientId,
                               Balance = c.Balance,
                               ClientId = ClientId,
                               Main = c.Main,
                               Identifier = c.Identifier
                            };

            return cuentas.ToList();
        }

        public async Task<SaveCuentaDeAhorroViewModel> GetByClientId(int ClientId)
        {
            var cuentaList = await _repository.GetAll();

            var cuenta = cuentaList.FirstOrDefault(c => c.ClientId == ClientId);

            SaveCuentaDeAhorroViewModel cuentaVm = _mapper.Map<SaveCuentaDeAhorroViewModel>(cuenta);

            return cuentaVm;
        }

        public async Task<CuentaDeAhorroViewModel> GetByIdentifier(int identifier)
        {
            return _mapper.Map<CuentaDeAhorroViewModel>(await _repository.GetByIdentifierAsync(identifier));
        }

        public async Task<List<int>> GetAllIdentifiers()
        {
            return await _repository.GetAllIdentifiersAsync();
        }

        public async Task<List<CuentaDeAhorroViewModel>> GetProductViewModelByClientId(int clienteId)
        {
            var list = await _repository.GetProductByClientIdAsync(clienteId);

            return _mapper.Map<List<CuentaDeAhorroViewModel>>(list);
        }
    }
}
