﻿using System;
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

            builder
                .Property((c) => c.Id)
                .HasDefaultValueSql("NEWSEQUENTIALID()")
                .ValueGeneratedOnAdd();

            builder
                .Property((c) => c.UsuarioId)
                .IsRequired();

            builder
                .Property((c) => c.Descricao)
                .HasMaxLength(200)
                .IsRequired();

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
