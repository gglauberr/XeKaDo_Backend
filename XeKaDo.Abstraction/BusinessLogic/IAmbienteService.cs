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
    public interface IAmbienteService
    {
        Task CriarAmbiente(ClaimsPrincipal user, AmbienteDTO req);

        Task AtualizarAmbiente(ClaimsPrincipal user, AmbienteDTO req);

        Task<AmbienteDTO> BuscarAmbiente(ClaimsPrincipal user, Guid ambienteId);

        Task<IPagedList<AmbienteDTO>> ListarAmbientes(ClaimsPrincipal user, ListarAmbienteRequest req);
    }
}
