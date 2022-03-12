using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Infrastructure;

namespace XeKaDo.Domain.Request
{
    public class ListarCategoriaEventoRequest
    {
        public PageInfo PageInfo { get; set; }
        public string Descricao { get; set; }
    }
}
