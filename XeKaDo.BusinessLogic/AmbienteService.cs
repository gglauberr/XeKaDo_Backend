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
    public class AmbienteService : IAmbienteService
    {
        private readonly XekadoContext context;

        public AmbienteService(
              XekadoContext context
        )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CriarAmbiente(ClaimsPrincipal user, AmbienteDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                ValidaCriarAtualizar(usuarioId, req);

                var existeAmbiente = await context.Ambiente.AnyAsync((a) => a.EventoID == req.EventoID && a.Descricao.Equals(req.Descricao));
                if (existeAmbiente) throw new XekadoException("Ambiente já está cadastrado");

                context.Add(new Ambiente()
                {
                    Descricao = req.Descricao,
                    Quantidade = req.Quantidade,
                    EventoID = req.EventoID
                });

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao criar ambiente", ex);
            }
        }

        public async Task AtualizarAmbiente(ClaimsPrincipal user, AmbienteDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                if (req.Id == Guid.Empty) throw new XekadoException("O id do ambiente não foi informado.");

                ValidaCriarAtualizar(usuarioId, req);

                var existeAmbiente = await context.Ambiente.AnyAsync((a) => a.Ativo && a.EventoID == req.EventoID && a.Id != req.Id && a.Descricao.Equals(req.Descricao));
                if (existeAmbiente) throw new XekadoException("Ambiente já está cadastrado");

                var ambiente = await context.Ambiente.Where((a) => a.EventoID == req.EventoID && a.Id == req.Id).FirstOrDefaultAsync();
                if (ambiente is null) throw new XekadoException("Ambiente não encontrado");

                ambiente.Descricao = req.Descricao;
                ambiente.Quantidade = req.Quantidade;

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao criar ambiente", ex);
            }
        }

        public async Task<AmbienteDTO> BuscarAmbiente(ClaimsPrincipal user, Guid ambienteId)
        {
            try
            {
                var usuarioId = user.GetId();

                if (ambienteId == Guid.Empty) throw new XekadoException("O id do ambiente não foi informado.");

                var ambiente = await context.Ambiente.Where((a) => a.Evento.UsuarioId == usuarioId && a.Id == ambienteId).FirstOrDefaultAsync();
                if (ambiente is null) throw new XekadoException("Ambiente não encontrado");

                return new AmbienteDTO()
                {
                    Id = ambiente.Id,
                    Descricao = ambiente.Descricao,
                    Quantidade = ambiente.Quantidade,
                    EventoID = ambiente.EventoID,
                    DataCriacao = ambiente.DataCriacao,
                    Ativo = ambiente.Ativo
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao criar ambiente", ex);
            }
        }

        public async Task<IPagedList<AmbienteDTO>> ListarAmbientes(ClaimsPrincipal user, ListarAmbienteRequest req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");

                if (req.EventoId == Guid.Empty) throw new XekadoException("Evento não informado");

                var query = context.Ambiente.Where((c) => c.Ativo && c.EventoID == req.EventoId).AsQueryable();

                if (!string.IsNullOrWhiteSpace(req.Descricao))
                    query = query.Where((q) => q.Descricao.Contains(req.Descricao));

                return await query
                        .Select((q) => new AmbienteDTO()
                        {
                            Id = q.Id,
                            Descricao = q.Descricao,
                            Quantidade = q.Quantidade,
                            EventoID = q.EventoID,
                            DataCriacao = q.DataCriacao,
                            Ativo = q.Ativo
                        })
                        .ToPagedListAsync(req.PageInfo.PageSize, req.PageInfo.PageNumber);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao listar ambientes de evento", ex);
            }
        }

        private void ValidaCriarAtualizar(Guid usuarioId, AmbienteDTO req)
        {
            if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
            if (req.EventoID == Guid.Empty) throw new XekadoException("O evento é obrigatório.");
            if (string.IsNullOrWhiteSpace(req.Descricao)) throw new XekadoException("A descrição é obrigatória.");
            if (req.Quantidade == 0) throw new XekadoException("A quantidade é obrigatória.");
        }
    }
}
