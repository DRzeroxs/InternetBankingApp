using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Interfaces.IServices
{
    public interface IBeneficiarioService : IGenericService<BeneficiarioViewModel, SaveBeneficiarioViewModel, Beneficiario>
    {
        Task<List<ClientBeneficiaryViewModel>> GetBeneficiaryList(int Id);
       
    }
}
