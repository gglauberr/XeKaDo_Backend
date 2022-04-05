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
using XeKaDo.CrossCutting.Extensions.Hash;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Enum;
using XeKaDo.Domain.Infrastructure;
using XeKaDo.Domain.Models;
using XeKaDo.Domain.Request;
using XeKaDo.EF.Context;

namespace XeKaDo.BusinessLogic
{
    public class ConvidadoService : IConvidadoService
    {
        private readonly XekadoContext context;

        public ConvidadoService(
              XekadoContext context
        )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CriarConvidado(ClaimsPrincipal user, ConvidadoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                ValidaCriarAlterar(usuarioId, req);

                var existeConvidado = await context.Convidado.AnyAsync((c) => c.Ativo && c.AmbienteId == req.AmbienteId && c.Nome.Contains(req.Nome));
                if (existeConvidado) throw new XekadoException("O convidado já está cadastrado");

                context.Add(new Convidado()
                {
                    Nome = req.Nome,
                    Celular = req.Celular,
                    Pagante = req.Pagante,
                    EnviadoConfirmacao = false,
                    StatusConfirmacao = EStatusConfirmacao.NaoConfirmado,
                    Observacao = req.Observacao,
                    Hash = $"{req.Nome}{req.Celular}{DateTime.Now.ToString("dd/MM/yyyy")}".SHA512(),
                    HashValidado = false,
                    AmbienteId = req.AmbienteId
                });

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao criar convidado", ex);
            }
        }

        public async Task AtualizarConvidado(ClaimsPrincipal user, ConvidadoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                if (req.Id == Guid.Empty) throw new XekadoException("O id não foi informado.");

                ValidaCriarAlterar(usuarioId, req);

                var existeConvidado = await context.Convidado.AnyAsync((c) => c.Ativo && c.AmbienteId == req.AmbienteId && c.Id != req.Id && c.Nome.Contains(req.Nome));
                if (existeConvidado) throw new XekadoException("O convidado já está cadastrado");

                var convidado = await context.Convidado.Where((c) => c.AmbienteId == req.AmbienteId && c.Id == req.Id).FirstOrDefaultAsync();
                if (convidado is null) throw new XekadoException("Convidado não encontrado");

                convidado.Nome = req.Nome;
                convidado.Celular = req.Celular;
                convidado.Pagante = req.Pagante;
                convidado.EnviadoConfirmacao = req.EnviadoConfirmacao;
                convidado.StatusConfirmacao = req.StatusConfirmacao;
                convidado.Observacao = req.Observacao;
                convidado.HashValidado = req.HashValidado;
                convidado.AmbienteId = req.AmbienteId;
                convidado.Ativo = req.Ativo;

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao alterar convidado", ex);
            }
        }

        public async Task<ConvidadoDTO> BuscarConvidado(ClaimsPrincipal user, Guid convidadoId)
        {
            try
            {
                var usuarioId = user.GetId();

                if (convidadoId == Guid.Empty) throw new XekadoException("O id não foi informado.");

                var convidado = await context.Convidado.Where((c) => c.Ambiente.Evento.UsuarioId == usuarioId && c.Id == convidadoId).FirstOrDefaultAsync();
                if (convidado is null) throw new XekadoException("Convidado não encontrado");

                return new ConvidadoDTO()
                {
                    Id = convidado.Id,
                    Nome = convidado.Nome,
                    Celular = convidado.Celular,
                    Pagante = convidado.Pagante,
                    EnviadoConfirmacao = convidado.EnviadoConfirmacao,
                    StatusConfirmacao = convidado.StatusConfirmacao,
                    Observacao = convidado.Observacao,
                    HashValidado = convidado.HashValidado,
                    AmbienteId = convidado.AmbienteId,
                    DataCriacao = convidado.DataCriacao,
                    Ativo = convidado.Ativo
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao buscar convidado", ex);
            }
        }

        public async Task<IPagedList<ConvidadoDTO>> ListarConvidados(ClaimsPrincipal user, ListarConvidadoRequest req)
        {
            try
            {
                var usuarioId = user.GetId();

                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (req.AmbienteId == Guid.Empty) throw new XekadoException("Ambiente não informado");

                var query = context.Convidado.Where((c) => c.Ativo && c.AmbienteId == req.AmbienteId).AsQueryable();

                if (!string.IsNullOrWhiteSpace(req.Nome))
                    query = query.Where((q) => q.Nome.Contains(req.Nome));

                if (req.Pagante.HasValue)
                    query = query.Where((q) => q.Pagante == req.Pagante);

                if (req.EnviadoConfirmacao.HasValue)
                    query = query.Where((q) => q.EnviadoConfirmacao == req.EnviadoConfirmacao);

                if (req.StatusConfirmacao.HasValue)
                    query = query.Where((q) => q.StatusConfirmacao == req.StatusConfirmacao);

                return await query
                        .Select((q) => new ConvidadoDTO()
                        {
                            Id = q.Id,
                            Nome = q.Nome,
                            Celular = q.Celular,
                            Pagante = q.Pagante,
                            EnviadoConfirmacao = q.EnviadoConfirmacao,
                            StatusConfirmacao = q.StatusConfirmacao,
                            Observacao = q.Observacao,
                            HashValidado = q.HashValidado,
                            AmbienteId = q.AmbienteId,
                            DataCriacao = q.DataCriacao,
                            Ativo = q.Ativo
                        })
                        .OrderBy((q) => q.Nome)
                        .ToPagedListAsync(req.PageInfo.PageSize, req.PageInfo.PageNumber);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao listar convidados", ex);
            }
        }

        private void ValidaCriarAlterar(Guid usuarioId, ConvidadoDTO req)
        {
            if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
            if (string.IsNullOrWhiteSpace(req.Nome)) throw new XekadoException("O nome é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Celular)) throw new XekadoException("O celular é obrigatório");
            if (req.AmbienteId == Guid.Empty) throw new XekadoException("O ambiente é obrigatório");
        }
    }
}
