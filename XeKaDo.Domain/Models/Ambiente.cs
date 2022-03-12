using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Models
{
    public class Ambiente
    {
        public Ambiente()
        {
            Convidados = new HashSet<Convidado>();
        }

        public Guid Id { get; set; }
        public Guid EventoID { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public virtual Evento Evento { get; set; }
        public virtual ICollection<Convidado> Convidados { get; set; }
    }
}
