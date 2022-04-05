using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Enum;

namespace XeKaDo.Domain.DTO
{
    public class ConvidadoDTO
    {
        public Guid Id { get; set; }
        public Guid AmbienteId { get; set; }
        public string Nome { get; set; }
        public string Celular { get; set; }
        public bool Pagante { get; set; }
        public bool EnviadoConfirmacao { get; set; }
        public EStatusConfirmacao StatusConfirmacao { get; set; }
        public string Observacao { get; set; }
        public bool HashValidado { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
