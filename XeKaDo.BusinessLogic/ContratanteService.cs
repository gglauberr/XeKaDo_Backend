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
    public class ContratanteService : IContratanteService
    {
        private readonly XekadoContext context;
        public ContratanteService(
              XekadoContext context
        )
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CriarContratante(ClaimsPrincipal user, ContratanteDTO req)
        {
            try
            {
                var usuarioId = user.GetId();

                ValidaCriarAtualizar(usuarioId, req);

                var existeContratante = await context.Contratante.AnyAsync((c) => c.Ativo && c.UsuarioId == usuarioId && c.Cpf == req.Cpf);
                if (existeContratante) throw new XekadoException("O contratante já está cadastrado");

                context.Add(new Contratante()
                {
                    UsuarioId = usuarioId,
                    Nome = req.Nome,
                    Cpf = req.Cpf,
                    Celular = req.Celular,
                    Email = req.Email,
                    Logradouro = req.Logradouro,
                    Numero = req.Numero,
                    Complemento = req.Complemento,
                    Cep = req.Cpf,
                    Bairro = req.Bairro,
                    Cidade = req.Cidade,
                    Uf = req.Uf
                });

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao criar contratante", ex);
            }
        }

        public async Task AtualizarContratante(ClaimsPrincipal user, ContratanteDTO req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (req.Id == Guid.Empty) throw new XekadoException("Informe o id da categoria");

                ValidaCriarAtualizar(usuarioId, req);

                var existeContratante = await context.Contratante.AnyAsync((c) => c.Ativo && c.UsuarioId == usuarioId && c.Id != req.Id && c.Cpf == req.Cpf);
                if (existeContratante) throw new XekadoException("O contrantante já está cadastrado");

                var contratante = await context.Contratante.FindAsync(req.Id);
                if (contratante is null) throw new XekadoException("Contratante não encontrada");

                contratante.Nome = req.Nome;
                contratante.Cpf = req.Cpf;
                contratante.Celular = req.Celular;
                contratante.Email = req.Email;
                contratante.Logradouro = req.Logradouro;
                contratante.Numero = req.Numero;
                contratante.Complemento = req.Complemento;
                contratante.Cep = req.Cpf;
                contratante.Bairro = req.Bairro;
                contratante.Cidade = req.Cidade;
                contratante.Uf = req.Uf;
                contratante.Ativo = req.Ativo;

                await context.SaveChangesAsync();
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao atualizar contratante", ex);
            }
        }

        public async Task<ContratanteDTO> BuscarContratante(ClaimsPrincipal user, Guid contratanteId)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
                if (contratanteId == Guid.Empty) throw new XekadoException("Informe o id do contratante");

                var contratante = await context.Contratante.Where((c) => c.UsuarioId == usuarioId && c.Id == contratanteId).FirstOrDefaultAsync();
                if (contratante is null) throw new XekadoException("Contratante não encontrada");

                return new ContratanteDTO()
                {
                    Id = contratante.Id,
                    UsuarioId = contratante.UsuarioId,
                    Nome = contratante.Nome,
                    Cpf = contratante.Cpf,
                    Celular = contratante.Celular,
                    Email = contratante.Email,
                    Logradouro = contratante.Logradouro,
                    Numero = contratante.Numero,
                    Complemento = contratante.Complemento,
                    Cep = contratante.Cpf,
                    Bairro = contratante.Bairro,
                    Cidade = contratante.Cidade,
                    Uf = contratante.Uf,
                    DataCriacao = contratante.DataCriacao,
                    Ativo = contratante.Ativo
                };
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao buscar contratante", ex);
            }
        }

        public async Task<IPagedList<ContratanteDTO>> ListarContratante(ClaimsPrincipal user, ListarContratanteRequest req)
        {
            try
            {
                var usuarioId = user.GetId();
                if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");

                var query = context.Contratante.Where((c) => c.Ativo && c.UsuarioId == usuarioId).AsQueryable();

                if (!string.IsNullOrWhiteSpace(req.Nome))
                    query = query.Where((q) => q.Nome.Contains(req.Nome));

                if (!string.IsNullOrWhiteSpace(req.Cpf))
                    query = query.Where((q) => q.Cpf.Contains(req.Cpf));

                if (!string.IsNullOrWhiteSpace(req.Celular))
                    query = query.Where((q) => q.Celular.Contains(req.Celular));

                if (!string.IsNullOrWhiteSpace(req.Email))
                    query = query.Where((q) => q.Email.Contains(req.Email));

                return await query
                        .Select((q) => new ContratanteDTO()
                        {
                            Id = q.Id,
                            UsuarioId = q.UsuarioId,
                            Nome = q.Nome,
                            Cpf = q.Cpf,
                            Celular = q.Celular,
                            Email = q.Email,
                            Logradouro = q.Logradouro,
                            Numero = q.Numero,
                            Complemento = q.Complemento,
                            Cep = q.Cpf,
                            Bairro = q.Bairro,
                            Cidade = q.Cidade,
                            Uf = q.Uf,
                            DataCriacao = q.DataCriacao,
                            Ativo = q.Ativo
                        })
                        .ToPagedListAsync(req.PageInfo.PageSize, req.PageInfo.PageNumber);
            }
            catch (XekadoException) { throw; }
            catch (Exception ex)
            {
                throw new XekadoException("Erro ao listar contratante", ex);
            }
        }

        private void ValidaCriarAtualizar(Guid usuarioId, ContratanteDTO req)
        {
            if (usuarioId == Guid.Empty) throw new XekadoException("Erro no usuário, Tente fazer o login novamente");
            if (string.IsNullOrWhiteSpace(req.Nome)) throw new XekadoException("O nome é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Cpf)) throw new XekadoException("O cpf é obrigatório");
            if (string.IsNullOrWhiteSpace(req.Celular)) throw new XekadoException("O celular é obrigatório");
        }
    }
}
