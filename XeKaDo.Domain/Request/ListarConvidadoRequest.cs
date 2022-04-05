using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Enum;
using XeKaDo.Domain.Infrastructure;

namespace XeKaDo.Domain.Request
{
    public class ListarConvidadoRequest
    {
        public PageInfo PageInfo { get; set; }
        public Guid AmbienteId { get; set; }
        public string Nome { get; set; }
        public bool? Pagante { get; set; }
        public bool? EnviadoConfirmacao { get; set; }
        public EStatusConfirmacao? StatusConfirmacao { get; set; }
    }
}
