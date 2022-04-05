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
    public interface IContratanteService
    {
        Task CriarContratante(ClaimsPrincipal user, ContratanteDTO req);

        Task AtualizarContratante(ClaimsPrincipal user, ContratanteDTO req);

        Task<ContratanteDTO> BuscarContratante(ClaimsPrincipal user, Guid contratanteId);

        Task<IPagedList<ContratanteDTO>> ListarContratantes(ClaimsPrincipal user, ListarContratanteRequest req);
    }
}
