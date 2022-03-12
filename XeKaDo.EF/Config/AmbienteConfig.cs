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
    public class AmbienteConfig : IEntityTypeConfiguration<Ambiente>
    {
        public void Configure(EntityTypeBuilder<Ambiente> builder)
        {
            builder.ToTable(nameof(Ambiente));

            builder.HasKey((a) => a.Id);

            builder
                .Property((a) => a.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .Property((a) => a.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((a) => a.DataCriacao)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property((a) => a.Ativo)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();

            builder
                .HasOne((a) => a.Evento)
                .WithMany((e) => e.Ambientes)
                .HasForeignKey((a) => a.EventoID);
        }
    }
}
