using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.DTO;

namespace XeKaDo.Domain.Response
{
    public class BuscarCategoriaEventoResponse : BaseResponse<CategoriaEventoDTO> { }
    public class ListarCategoriaEventoResponse : BasePagedResponse<CategoriaEventoDTO> { }
}
