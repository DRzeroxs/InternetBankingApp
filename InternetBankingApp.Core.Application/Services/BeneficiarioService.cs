using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace InternetBankingApp.Core.Application.Services
{
    public class BeneficiarioService : GenericService<BeneficiarioViewModel, SaveBeneficiarioViewModel, Beneficiario> , IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICuentaDeAhorroRepository _cuentasAhorro;
        private readonly IClienteRepository _clienteRepository;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public BeneficiarioService(IBeneficiarioRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper, ICuentaDeAhorroRepository cuentasAhorro, IClienteRepository clienteRepository) : base(repository, mapper)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            _clienteRepository = clienteRepository;
            _cuentasAhorro = cuentasAhorro;
            userViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("user");
        }
        public async Task<List<ClientBeneficiaryViewModel>> GetBeneficiaryList(int Id)
        {
            var beneficiaryAllList = await _repository.GetAll();
            var cuentaAhorroList = await _cuentasAhorro.GetAll();   
            var clientList = await _clienteRepository.GetAll();

            var beneficiaryList = from b in beneficiaryAllList
                                  join ca in cuentaAhorroList on b.CuentaIdentifier equals ca.Identifier
                                  join c in clientList on ca.Id equals c.Id
                                  where b.ClienteId == Id
                                  select new ClientBeneficiaryViewModel()
                                  {
                                      Id = b.Id,
                                      Name = c.FirstName,
                                      LastName = c.LatsName,
                                      AccountNumber = b.CuentaIdentifier
                                  };
            return beneficiaryList.ToList();    
        }
        
    }
}
