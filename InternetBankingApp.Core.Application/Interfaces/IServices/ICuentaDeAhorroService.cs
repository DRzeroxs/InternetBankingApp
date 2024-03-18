﻿using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface ICuentaDeAhorroService : IGenericService<CuentaDeAhorroViewModel, SaveCuentaDeAhorroViewModel, CuentaDeAhorro>, IGetInfoProductsService<CuentaDeAhorroViewModel>
    {
        Task<List<CuentaDeAhorroViewModel>> GetListByClientId(int ClientId);
        Task<SaveCuentaDeAhorroViewModel> GetByClientId(int ClientId);

        Task<bool> ConfirnAccount(int identifier);
    }
}
