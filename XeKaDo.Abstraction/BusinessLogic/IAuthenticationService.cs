using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Request;

namespace XeKaDo.Abstraction.BusinessLogic
{
    public interface IAuthenticationService
    {
        Func<TokenValidatedContext, Task> OnTokenValidated { get; }

        Task<TokenDTO> CadastrarUsuario(RegisterRequest req);

        Task<TokenDTO> FazerLogin(LoginRequest req);
    }
}
