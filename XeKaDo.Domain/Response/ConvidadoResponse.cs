using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.DTO;

namespace XeKaDo.Domain.Response
{
    public class BuscarConvidadoResponse : BaseResponse<ConvidadoDTO> { }
    public class ListarConvidadoResponse : BasePagedResponse<ConvidadoDTO> { }
}
