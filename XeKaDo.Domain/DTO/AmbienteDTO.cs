using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.DTO
{
    public class AmbienteDTO
    {
        public Guid Id { get; set; }
        public Guid EventoID { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
