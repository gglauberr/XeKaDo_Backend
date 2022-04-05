using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Infrastructure;

namespace XeKaDo.Domain.Request
{
    public class ListarEventoRequest
    {
        public PageInfo PageInfo { get; set; }
        public Guid? ContratanteId { get; set; }
        public Guid? CategoriaEventoId { get; set; }
        public string Descricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
