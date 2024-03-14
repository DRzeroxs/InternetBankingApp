﻿using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Helpers;
using InternetBankingApp.Core.Application.Interfaces.IRepository;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class CuentaDeAhorroService : GenericService<SaveCuentaDeAhorroViewModel, CuentaDeAhorroViewModel, CuentaDeAhorro>, ICuentaDeAhorroService
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
    }
}