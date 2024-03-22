﻿using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IRepository
{
    public interface ICuentaDeAhorroRepository : IGenericRepository<CuentaDeAhorro> , IGetInfoProductsRepository<CuentaDeAhorro>
    {
        Task<CuentaDeAhorro> GetMainByClientIdAsync(int clientId);
    }
}
