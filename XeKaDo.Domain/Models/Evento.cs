using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Models
{
    public class Evento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public DateTime DataEvento { get; set; }
        public TimeSpan HoraEvento { get; set; }
        public DateTime DataLimiteConfirmacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
