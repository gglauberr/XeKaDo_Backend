using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Abstraction.BusinessLogic;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Infrastructure;
using XeKaDo.Domain.Models;
using XeKaDo.Domain.Request;

namespace XeKaDo.BusinessLogic
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IConfiguration configuration;
        public AuthenticationService(
              UserManager<User> userManager
            , SignInManager<User> signInManager
            , IConfiguration configuration
        )
        {
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Func<TokenValidatedContext, Task> OnTokenValidated
        {
            get
            {
                return async (tvc) =>
                {
                    var teste = await Task.FromResult("");

                    return;
                };
            }
        }

        public async Task<TokenDTO> CadastrarUsuario(RegisterRequest req)
        {
            try
            {
                ValidarCadastroUsuario(req);

                var user = await userManager.FindByEmailAsync(req.Email);

                if (user != null) throw new XekadoException("O usuário já está cadastrado");

                user = new User()
                {
                    UserName = req.Nome,
                    Email = req.Email
                };

                var result = await userManager.CreateAsync(user, req.Senha);

                if (!result.Succeeded) throw new XekadoException("Erro ao criar usuário");

                var token = GerarToken(user);

                return new TokenDTO()
                {
                    Token = token
                };
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao cadastrar usuário", ex);
            }
        }

        public async Task<TokenDTO> FazerLogin(LoginRequest req)
        {
            try
            {
                ValidarLogin(req);

                var user = await userManager.FindByEmailAsync(req.Email);

                if (user is null || await userManager.IsLockedOutAsync(user)) throw new XekadoException("Erro ao efetuar login");

                if (!await userManager.CheckPasswordAsync(user, req.Senha)) throw new XekadoException("Login ou Senha incorreto.");

                var result = await signInManager.CheckPasswordSignInAsync(user, req.Senha, true);

                if (!result.Succeeded) throw new XekadoException("Erro ao efetuar login");

                var token = GerarToken(user);

                return new TokenDTO()
                {
                    Token = token
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao fazer login", ex);
            }
        }

        private void ValidarCadastroUsuario(RegisterRequest req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.Nome)) throw new XekadoException("O nome é obrigatório");
                if (string.IsNullOrWhiteSpace(req.Email)) throw new XekadoException("O email é obrigatório");
                if (string.IsNullOrWhiteSpace(req.Senha)) throw new XekadoException("Digite a senha");
                if (string.IsNullOrWhiteSpace(req.ConfirmSenha)) throw new XekadoException("Confirme a sua senha");
                if (req.Senha.ToUpper() != req.ConfirmSenha.ToUpper()) throw new XekadoException("As senhas estão divergentes");
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao validar o cadastro de usuário", ex);
            }
        }

        private void ValidarLogin(LoginRequest req)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(req.Email)) throw new XekadoException("Digite o email");
                if (string.IsNullOrWhiteSpace(req.Senha)) throw new XekadoException("Digite a senha");
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao validar o cadastro de usuário", ex);
            }
        }

        private string GerarToken(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    configuration.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescription);

                return tokenHandler.WriteToken(token);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao gerar token", ex);
            }
        }
    }
}
