using AutoMapper;
using InternetBankingApp.Core.Application.Dtos.Account;
using InternetBankingApp.Core.Application.ViewModels.Beneficiario;
using InternetBankingApp.Core.Application.ViewModels.Cliente;
using InternetBankingApp.Core.Application.ViewModels.CuentaDeAhorro;
using InternetBankingApp.Core.Application.ViewModels.Prestamo;
using InternetBankingApp.Core.Application.ViewModels.TarjetaDeCredito;
using InternetBankingApp.Core.Application.ViewModels.Transaccion;
using InternetBankingApp.Core.Application.ViewModels.User;
using InternetBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetBankingApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {

            #region Beneficiario
            CreateMap<Beneficiario, SaveBeneficiarioViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());

            CreateMap<Beneficiario, BeneficiarioViewModel>()
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());
            #endregion

            #region Cliente
            CreateMap<Cliente, SaveClienteViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Transacciones, opt => opt.Ignore())
                .ForMember(x => x.TarjetasDeCredito, opt => opt.Ignore())
                .ForMember(x => x.Prestamo, opt => opt.Ignore())
                .ForMember(x => x.CuentasDeAhorro, opt => opt.Ignore())
                .ForMember(x => x.Beneficiarios, opt => opt.Ignore());

            CreateMap<Cliente, ClienteViewModel>()
                .ForMember(x => x.Beneficiarios, opt => opt.Ignore())
                .ForMember(x => x.Cuentas, opt => opt.Ignore())
                .ForMember(x => x.Prestamos, opt => opt.Ignore())
                .ForMember(x => x.Tarjetas, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Transacciones, opt => opt.Ignore())
                .ForMember(x => x.TarjetasDeCredito, opt => opt.Ignore())
                .ForMember(x => x.Prestamo, opt => opt.Ignore())
                .ForMember(x => x.CuentasDeAhorro, opt => opt.Ignore())
                .ForMember(x => x.Beneficiarios, opt => opt.Ignore());
            #endregion

            #region CuentaDeAhorro
            CreateMap<CuentaDeAhorro, SaveCuentaDeAhorroViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());

            CreateMap<CuentaDeAhorro, CuentaDeAhorroViewModel>()
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());


            CreateMap<CuentaDeAhorroViewModel, SaveCuentaDeAhorroViewModel>()
                .ReverseMap()
                .ForMember(x => x.Cliente, opt => opt.Ignore());
            #endregion

            #region Prestamo
            CreateMap<Prestamo, SavePrestamoViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());

            CreateMap<Prestamo, PrestamoViewModel>()
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());
            #endregion

            #region TarjetaDeCredito
            CreateMap<TarjetaDeCredito, SaveTarjetaDeCreditoViewModel>()
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());

            CreateMap<TarjetaDeCredito, TarjetaDeCreditoViewModel>()
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());
            #endregion

            #region Transaccion
            CreateMap<Transaccion, SaveTransaccionViewModel>()
                .ForMember(x => x.Cliente, opt => opt.Ignore())
                .ForMember(x => x.CuentaDestino, opt => opt.Ignore())
                .ForMember(x => x.CuentaOrige, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.Ignore())
                .ForMember(x => x.LastName, opt => opt.Ignore())
                .ForMember(x => x.userId, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.CreatedDate, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
                .ForMember(x => x.CuentaDestino, opt => opt.Ignore())
                .ForMember(x => x.CuentaOrigen, opt => opt.Ignore())
                .ForMember(x => x.Cliente, opt => opt.Ignore());

            CreateMap<Transaccion, TransaccionViewModel>()
               .ForMember(x => x.Cliente, opt => opt.Ignore())
               .ForMember(x => x.CuentaDestino, opt => opt.Ignore())
               .ForMember(x => x.CuentaOrigen, opt => opt.Ignore())
               .ReverseMap()
               .ForMember(x => x.CreatedDate, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedDate, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedby, opt => opt.Ignore())
               .ForMember(x => x.CuentaDestino, opt => opt.Ignore())
               .ForMember(x => x.CuentaOrigen, opt => opt.Ignore())
               .ForMember(x => x.Cliente, opt => opt.Ignore());
            #endregion

            #region "User"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegistrerRequest, RegisterViewModel>()
               .ForMember(x => x.HasError, opt => opt.Ignore())
               .ForMember(x => x.Error, opt => opt.Ignore())
               .ReverseMap();

            CreateMap<UserViewModel, EditClientViewModel>()
               .ForMember(opt => opt.AddAmount, i => i.Ignore()).ReverseMap();

            CreateMap<UserViewModel, EditAdminViewModel>()
            .ReverseMap();
            #endregion

        }
    }
}
