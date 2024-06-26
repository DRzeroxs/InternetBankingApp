﻿using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.Interfaces.IAccount;
using InternetBankingApp.Core.Application.Interfaces.IServices;
using InternetBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;

        public UserService(IAccountServices accountServices, IMapper mapper)
        {
            _accountServices = accountServices;
            _mapper = mapper;
        }

        // Metodo de logueo
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel loginVm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(loginVm);
            AuthenticationResponse authenticationResponse = await _accountServices.AuthenticateASYNC(loginRequest);
            return authenticationResponse;
        }
        // Metodo para Registrar Usuario Administrador
        public async Task<RegistrerResponse> RegisterAdminAsync(RegisterViewModel vm, string origin)
        {
            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerAdminUserAsync(registerRequest, origin);
        }

        // Metodo para Registrar Usuario Cliente
        public async Task<RegistrerResponse> RegisterCustomerAsync(RegisterViewModel vm, string origin)
        {

            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerCustomerUserAsync(registerRequest, origin);
        }

            // Metodo de deslogueo
            public async Task SignOutAsync()
        {
            await _accountServices.SingOutAsync();
        }

    }
}
