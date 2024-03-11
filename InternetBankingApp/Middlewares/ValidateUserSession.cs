using InternetBankingApp.Core.Application.Dtos.Account;
using Microsoft.AspNetCore.Http;
using InternetBankingApp.Core.Application.Helpers;

namespace InternetBankingApp.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasUser()
        {
            AuthenticationResponse usuarioViewModel = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("Admin");

            if (usuarioViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
