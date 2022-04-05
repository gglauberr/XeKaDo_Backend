using Microsoft.EntityFrameworkCore;
using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Abstraction.BusinessLogic;
using XeKaDo.CrossCutting.Extensions;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Infrastructure;
using XeKaDo.Domain.Models;
using XeKaDo.Domain.Request;
using XeKaDo.EF.Context;

namespace XeKaDo.BusinessLogic
{
    public class CategoriaEventoService : ICategoriaEventoService
    {
        private readonly XekadoContext context;
        public CategoriaEventoService(
              XekadoContext context
        )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CriarCategoria(ClaimsPrincipal user, CategoriaEventoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (string.IsNullOrWhiteSpace(req.Descricao)) throw new XekadoException("A descrição é obrigatório");

                var existeCategoria = await context.CategoriaEvento.AnyAsync((c) => c.Ativo && c.UsuarioId == usuarioId && c.Descricao == req.Descricao);
                if (existeCategoria) throw new XekadoException("A categoria do evento já está cadastrada");

                context.Add(new CategoriaEvento()
                {
                    UsuarioId = usuarioId,
                    Descricao = req.Descricao          
                });

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao criar categoria de evento", ex);
            }
        }

        public async Task AtualizarCategoria(ClaimsPrincipal user, CategoriaEventoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (req.Id == Guid.Empty) throw new XekadoException("Informe o id da categoria");
                if (string.IsNullOrWhiteSpace(req.Descricao)) throw new XekadoException("A descrição é obrigatório");

                var existeCategoria = await context.CategoriaEvento.AnyAsync((c) => c.Ativo && c.UsuarioId == usuarioId && c.Id != req.Id && c.Descricao == req.Descricao);
                if (existeCategoria) throw new XekadoException("A categoria de evento já está cadastrada");

                var categoria = await context.CategoriaEvento.FindAsync(req.Id);
                if (categoria is null) throw new XekadoException("Categoria não encontrada");

                categoria.Descricao = req.Descricao;
                categoria.Ativo = req.Ativo;

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao atualizar categoria de evento", ex);
            }
        }

        public async Task<CategoriaEventoDTO> BuscarCategoria(ClaimsPrincipal user, Guid categoriaId)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (categoriaId == Guid.Empty) throw new XekadoException("Informe o id da categoria");

                var categoria = await context.CategoriaEvento.Where((c) => c.UsuarioId == usuarioId && c.Id == categoriaId).FirstOrDefaultAsync();
                if (categoria is null) throw new XekadoException("Categoria não encontrada");

                return new CategoriaEventoDTO()
                {
                    Id = categoria.Id,
                    UsuarioId = categoria.UsuarioId,
                    Descricao = categoria.Descricao,
                    DataCriacao = categoria.DataCriacao,
                    Ativo = categoria.Ativo
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao buscar categoria de evento", ex);
            }
        }

        public async Task<IPagedList<CategoriaEventoDTO>> ListarCategoria(ClaimsPrincipal user, ListarCategoriaEventoRequest req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");

                var query = context.CategoriaEvento.Where((c) => c.Ativo && c.UsuarioId == usuarioId).AsQueryable();

                if (!string.IsNullOrWhiteSpace(req.Descricao))
                    query = query.Where((q) => q.Descricao.Contains(req.Descricao));

                return await query
                        .Select((q) => new CategoriaEventoDTO()
                        {
                            Id = q.Id,
                            UsuarioId = q.UsuarioId,
                            Descricao = q.Descricao,
                            DataCriacao = q.DataCriacao,
                            Ativo = q.Ativo
                        })
                        .ToPagedListAsync(req.PageInfo.PageSize, req.PageInfo.PageNumber);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao listar categoria de evento", ex);
            }
        }
    }
}
