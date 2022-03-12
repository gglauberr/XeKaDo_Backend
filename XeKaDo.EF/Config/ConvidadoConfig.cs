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
    public class ConvidadoConfig : IEntityTypeConfiguration<Convidado>
    {
        public void Configure(EntityTypeBuilder<Convidado> builder)
        {
            builder.ToTable(nameof(Convidado));

            builder.HasKey((c) => c.Id);

            builder
                .Property((c) => c.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .Property((c) => c.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((c) => c.Celular)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property((c) => c.StatusConfirmacao)
                .IsRequired();

            builder
                .Property((c) => c.Observacao)
                .HasMaxLength(1000);

            builder
                .Property((c) => c.Hash)
                .HasMaxLength(512)
                .IsRequired();

            builder
                .Property((c) => c.DataCriacao)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property((c) => c.Ativo)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();

            builder
                .HasOne((c) => c.Ambiente)
                .WithMany((a) => a.Convidados)
                .HasForeignKey((c) => c.AmbienteId);
        }
    }
}
