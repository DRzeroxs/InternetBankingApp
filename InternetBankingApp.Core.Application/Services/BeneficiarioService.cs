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
    public class BeneficiarioService : GenericService<SaveBeneficiarioViewModel, BeneficiarioViewModel, Beneficiario> , IBeneficiarioService
    {
        private readonly IBeneficiarioRepository _repository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AuthenticationResponse userViewModel;
        private readonly IMapper _mapper;

        public BeneficiarioService(IBeneficiarioRepository repository, IHttpContextAccessor contextAccessor, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _contextAccessor = contextAccessor;
            _mapper = mapper;
            userViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("user");
        }
    }
}
