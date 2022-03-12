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
    public class EventoConfig : IEntityTypeConfiguration<Evento>
    {
        public void Configure(EntityTypeBuilder<Evento> builder)
        {
            builder.ToTable(nameof(Evento));

            builder.HasKey((e) => e.Id);

            builder
                .Property((e) => e.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .Property((e) => e.UsuarioId)
                .IsRequired();

            builder
                .Property((e) => e.Descricao)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((e) => e.Logradouro)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((e) => e.Numero)
                .HasMaxLength(50);

            builder
                .Property((e) => e.Complemento)
                .HasMaxLength(200);

            builder
                .Property((e) => e.Cep)
                .HasMaxLength(9)
                .IsRequired();

            builder
                .Property((e) => e.Bairro)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((e) => e.Cidade)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((e) => e.Uf)
                .HasMaxLength(2)
                .IsRequired();

            builder
                .Property((e) => e.DataEvento)
                .IsRequired();

            builder
                .Property((e) => e.HoraEvento)
                .IsRequired();

            builder
                .Property((e) => e.DataLimiteConfirmacao)
                .IsRequired();

            builder
                .Property((e) => e.DataCriacao)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property((e) => e.Ativo)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();

            builder
                .HasOne((e) => e.Contratante)
                .WithMany((c) => c.Eventos)
                .HasForeignKey((e) => e.ContratanteId);

            builder
                .HasOne((e) => e.CategoriaEvento)
                .WithMany((c) => c.Eventos)
                .HasForeignKey((e) => e.CategoriaEventoId);
        }
    }
}
