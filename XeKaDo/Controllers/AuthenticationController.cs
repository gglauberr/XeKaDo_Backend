using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeKaDo.Abstraction.BusinessLogic;
using XeKaDo.Domain.Request;
using XeKaDo.Domain.Response;

namespace XeKaDo.Api.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService authenticationService;
        public AuthenticationController(
              IAuthenticationService authenticationService
        )
        {
            this.authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesDefaultResponseType(typeof(TokenResponse))]
        public async Task<IActionResult> CadastrarUsuario(RegisterRequest req)
        {
            var response = new TokenResponse();
            try
            {
                response.Data = await authenticationService.CadastrarUsuario(req);
                response.Success = true;
                response.Message = "Usuário cadastrado com sucesso";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesErrorResponseType(typeof(TokenResponse))]
        public async Task<IActionResult> FazerLogin(LoginRequest req)
        {
            var response = new TokenResponse();
            try
            {
                response.Data = await authenticationService.FazerLogin(req);
                response.Success = true;
                response.Message = "Login efetuado com sucesso";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
