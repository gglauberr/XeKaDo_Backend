using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Request;

namespace XeKaDo.Abstraction.BusinessLogic
{
    public interface IConvidadoService
    {
        Task CriarConvidado(ClaimsPrincipal user, ConvidadoDTO req);

        Task AtualizarConvidado(ClaimsPrincipal user, ConvidadoDTO req);

        Task<ConvidadoDTO> BuscarConvidado(ClaimsPrincipal user, Guid convidadoId);

        Task<IPagedList<ConvidadoDTO>> ListarConvidados(ClaimsPrincipal user, ListarConvidadoRequest req);
    }
}
