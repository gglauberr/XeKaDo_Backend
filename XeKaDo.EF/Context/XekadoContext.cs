using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Models;

namespace XeKaDo.EF.Context
{
    public class XekadoContext : IdentityDbContext<User>
    {
        public XekadoContext(DbContextOptions<XekadoContext> options) : base(options) { }

        public virtual DbSet<Contratante> Contratante { get; set; }
        public virtual DbSet<CategoriaEvento> CategoriaEvento { get; set; }
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Ambiente> Ambiente { get; set; }
        public virtual DbSet<Convidado> Convidado { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(XekadoContext).Assembly,
                (type) => type.Namespace.Contains("XeKaDo.EF"));

            base.OnModelCreating(builder);
        }
    }
}
