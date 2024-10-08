﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Models
{
    public class CategoriaEvento
    {
        public CategoriaEvento()
        {
            Eventos = new HashSet<Evento>();
        }

        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public virtual ICollection<Evento> Eventos { get; set; }
    }
}
