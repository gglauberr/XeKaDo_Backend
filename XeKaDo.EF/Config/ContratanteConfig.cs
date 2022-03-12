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
    public class ContratanteConfig : IEntityTypeConfiguration<Contratante>
    {
        public void Configure(EntityTypeBuilder<Contratante> builder)
        {
            builder.ToTable(nameof(Contratante));

            builder.HasKey((c) => c.Id);

            builder
                .Property((c) => c.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .Property((c) => c.UsuarioId)
                .IsRequired();

            builder
                .Property((c) => c.Nome)
                .HasMaxLength(200)
                .IsRequired();

            builder
                .Property((c) => c.Cpf)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property((c) => c.Celular)
                .HasMaxLength(11)
                .IsRequired();

            builder
                .Property((c) => c.Email)
                .HasMaxLength(200);

            builder
                .Property((c) => c.Logradouro)
                .HasMaxLength(200);

            builder
                .Property((c) => c.Numero)
                .HasMaxLength(50);

            builder
                .Property((c) => c.Complemento)
                .HasMaxLength(200);

            builder
                .Property((c) => c.Cep)
                .HasMaxLength(9);

            builder
                .Property((c) => c.Bairro)
                .HasMaxLength(200);

            builder
                .Property((c) => c.Cidade)
                .HasMaxLength(200);

            builder
                .Property((c) => c.Uf)
                .HasMaxLength(2);

            builder
                .Property((c) => c.DataCriacao)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property((c) => c.Ativo)
                .HasDefaultValue(true)
                .ValueGeneratedOnAdd();
        }
    }
}
