using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XeKaDo.Domain.Models;

namespace XeKaDo.EF.Config
{
    public class CategoriaEventoConfig : IEntityTypeConfiguration<CategoriaEvento>
    {
        public void Configure(EntityTypeBuilder<CategoriaEvento> builder)
        {
            builder.ToTable(nameof(CategoriaEvento));

            builder.HasKey((c) => c.Id);
        }
    }

    public class CategoriaEvento
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
