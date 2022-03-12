using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeKaDo.Abstraction.BusinessLogic;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Request;
using XeKaDo.Domain.Response;

namespace XeKaDo.Api.Controllers
{
    public class CategoriaEventoController : BaseController
    {
        private readonly ICategoriaEventoService categoriaEventoService;

        public CategoriaEventoController(
              ICategoriaEventoService categoriaEventoService
        )
        {
            this.categoriaEventoService = categoriaEventoService ?? throw new ArgumentNullException(nameof(categoriaEventoService));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> CriarCategoria(CategoriaEventoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await categoriaEventoService.CriarCategoria(User, req);
                response.Success = true;
                response.Message = "Categoria de evento criada com sucesso";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> AtualizarCategoria(CategoriaEventoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await categoriaEventoService.AtualizarCategoria(User, req);
                response.Success = true;
                response.Message = "Categoria de evento atualizada com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpGet("{categoriaId}")]
        [ProducesDefaultResponseType(typeof(BuscarCategoriaEventoResponse))]
        public async Task<IActionResult> BuscarCategoria(Guid categoriaId)
        {
            var response = new BuscarCategoriaEventoResponse();
            try
            {
                response.Data = await categoriaEventoService.BuscarCategoria(User, categoriaId);
                response.Success = true;
                response.Message = "Categoria de evento recuperada com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ListarCategoriaEventoResponse))]
        public async Task<IActionResult> ListarCategoria(ListarCategoriaEventoRequest req)
        {
            var response = new ListarCategoriaEventoResponse() { PageInfo = req.PageInfo };
            try
            {
                var categoria = await categoriaEventoService.ListarCategoria(User, req);
                response.Success = true;
                response.Message = "Lista de categoria de evento recuperada com sucesso";
                response.Data = categoria.ToList();
                response.ItensTotal = categoria.TotalCount;
                response.Pages = categoria.TotalPage;
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
