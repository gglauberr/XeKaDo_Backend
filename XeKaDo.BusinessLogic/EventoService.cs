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
    public class EventoService : IEventoService
    {
        private readonly XekadoContext context;

        public EventoService(
              XekadoContext context
        )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CriarEvento(ClaimsPrincipal user, EventoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                ValidaCriarAtualizar(usuarioId, req);

                context.Add(new Evento()
                {
                    UsuarioId = usuarioId,
                    Descricao = req.Descricao,
                    Logradouro = req.Logradouro,
                    Numero = req.Numero,
                    Complemento = req.Complemento,
                    Cep = req.Cep,
                    Bairro = req.Bairro,
                    Cidade = req.Cidade,
                    Uf = req.Uf,
                    DataEvento = req.DataEvento,
                    HoraEvento = req.HoraEvento,
                    DataLimiteConfirmacao = req.DataLimiteConfirmacao,
                    DataCriacao = req.DataCriacao,
                    Ativo = req.Ativo,
                    ContratanteId = req.ContratanteId,
                    CategoriaEventoId = req.CategoriaEventoId
                });

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao criar evento.", ex);
            }
        }

        public async Task AtualizarEvento(ClaimsPrincipal user, EventoDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                if (req.Id == Guid.Empty) throw new XekadoException("Informe o Id do evento.");

                ValidaCriarAtualizar(usuarioId, req);

                var evento = await context.Evento.Where((e) => e.UsuarioId == usuarioId && e.Id == req.Id).FirstOrDefaultAsync();
                if (evento is null) throw new XekadoException("Evento não encontrado");

                evento.Descricao = req.Descricao;
                evento.Logradouro = req.Logradouro;
                evento.Numero = req.Numero;
                evento.Complemento = req.Complemento;
                evento.Cep = req.Cep;
                evento.Bairro = req.Bairro;
                evento.Cidade = req.Cidade;
                evento.Uf = req.Uf;
                evento.DataEvento = req.DataEvento;
                evento.HoraEvento = req.HoraEvento;
                evento.DataLimiteConfirmacao = req.DataLimiteConfirmacao;
                evento.ContratanteId = req.ContratanteId;
                evento.CategoriaEventoId = req.CategoriaEventoId;

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch(Exception ex)
            {
                throw new XekadoException("Erro ao atualizar evento.", ex);
            }
        }

        public async Task<EventoDTO> BuscarEvento(ClaimsPrincipal user, Guid eventoId)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (eventoId == Guid.Empty) throw new XekadoException("Informe o id do evento");

                var evento = await context.Evento.Where((c) => c.UsuarioId == usuarioId && c.Id == eventoId).FirstOrDefaultAsync();
                if (evento is null) throw new XekadoException("Evento não encontrado");

                return new EventoDTO()
                {
                    Id = evento.Id,
                    UsuarioId = evento.UsuarioId,
                    Descricao = evento.Descricao,
                    Logradouro = evento.Logradouro,
                    Numero = evento.Numero,
                    Complemento = evento.Complemento,
                    Cep = evento.Cep,
                    Bairro = evento.Bairro,
                    Cidade = evento.Cidade,
                    Uf = evento.Uf,
                    DataEvento = evento.DataEvento,
                    HoraEvento = evento.HoraEvento,
                    DataLimiteConfirmacao = evento.DataLimiteConfirmacao,
                    DataCriacao = evento.DataCriacao,
                    Ativo = evento.Ativo,
                    ContratanteId = evento.ContratanteId,
                    CategoriaEventoId = evento.CategoriaEventoId
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao buscar evento", ex);
            }
        }

        public async Task<IPagedList<EventoDTO>> ListarEventos(ClaimsPrincipal user, ListarEventoRequest req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");

                var query = context.Evento.Where((c) => c.Ativo && c.UsuarioId == usuarioId).AsQueryable();

                if (req.ContratanteId.HasValue)
                    query = query.Where((q) => q.ContratanteId == req.ContratanteId);

                if (req.CategoriaEventoId.HasValue)
                    query = query.Where((q) => q.CategoriaEventoId == req.CategoriaEventoId);

                if (!string.IsNullOrWhiteSpace(req.Descricao))
                    query = query.Where((q) => q.Descricao.Contains(req.Descricao));

                if (req.DataInicio.HasValue)
                    query = query.Where((q) => q.DataEvento >= req.DataInicio);

                if (req.DataFim.HasValue)
                    query = query.Where((q) => q.DataEvento <= req.DataFim);

                return await query
                        .Select((q) => new EventoDTO()
                        {
                            Id = q.Id,
                            UsuarioId = q.UsuarioId,
                            Descricao = q.Descricao,
                            Logradouro = q.Logradouro,
                            Numero = q.Numero,
                            Complemento = q.Complemento,
                            Cep = q.Cep,
                            Bairro = q.Bairro,
                            Cidade = q.Cidade,
                            Uf = q.Uf,
                            DataEvento = q.DataEvento,
                            HoraEvento = q.HoraEvento,
                            DataLimiteConfirmacao = q.DataLimiteConfirmacao,
                            DataCriacao = q.DataCriacao,
                            Ativo = q.Ativo,
                            ContratanteId = q.ContratanteId,
                            CategoriaEventoId = q.CategoriaEventoId
                        })
                        .ToPagedListAsync(req.PageInfo.PageSize, req.PageInfo.PageNumber);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao listar eventos", ex);
            }
        }

        private void ValidaCriarAtualizar(Guid usuarioId, EventoDTO req)
        {
            if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
            if (string.IsNullOrWhiteSpace(req.Descricao)) throw new XekadoException("A Descrição é obrigatória");
            if (string.IsNullOrWhiteSpace(req.Logradouro)) throw new XekadoException("O logradouro é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Numero)) throw new XekadoException("O número é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Cep)) throw new XekadoException("O CEP é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Bairro)) throw new XekadoException("O bairro é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Cidade)) throw new XekadoException("A cidade é obrigatória");
            if (string.IsNullOrWhiteSpace(req.Uf)) throw new XekadoException("O estado é obrigatório");
            if (req.DataEvento == DateTime.MinValue) throw new XekadoException("A data do evento é obrigatório.");
            if (req.HoraEvento == TimeSpan.Zero) throw new XekadoException("A hora do evento é obrigatório");
            if (req.DataLimiteConfirmacao == DateTime.MinValue) throw new XekadoException("A data limite para confirmação é obrigatória");
            if (req.ContratanteId == Guid.Empty) throw new XekadoException("O contratante é obrigatório");
            if (req.CategoriaEventoId == Guid.Empty) throw new XekadoException("A categoria do evento é obrigatória");
        }
    }
}
