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
    public interface IEventoService
    {
        Task CriarEvento(ClaimsPrincipal user, EventoDTO req);

        Task AtualizarEvento(ClaimsPrincipal user, EventoDTO req);

        Task<EventoDTO> BuscarEvento(ClaimsPrincipal user, Guid eventoId);

        Task<IPagedList<EventoDTO>> ListarEventos(ClaimsPrincipal user, ListarEventoRequest req);
    }
}
