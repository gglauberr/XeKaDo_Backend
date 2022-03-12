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
    public interface ICategoriaEventoService
    {
        Task CriarCategoria(ClaimsPrincipal user, CategoriaEventoDTO req);

        Task AtualizarCategoria(ClaimsPrincipal user, CategoriaEventoDTO req);

        Task<CategoriaEventoDTO> BuscarCategoria(ClaimsPrincipal user, Guid categoriaId);

        Task<IPagedList<CategoriaEventoDTO>> ListarCategoria(ClaimsPrincipal user, ListarCategoriaEventoRequest req);
    }
}
