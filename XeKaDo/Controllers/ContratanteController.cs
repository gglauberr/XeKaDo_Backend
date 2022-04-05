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
    public class ContratanteController : BaseController
    {
        private readonly IContratanteService contratanteService;

        public ContratanteController(
              IContratanteService contratanteService
        )
        {
            this.contratanteService = contratanteService ?? throw new ArgumentNullException(nameof(contratanteService));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> CriarContratante(ContratanteDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await contratanteService.CriarContratante(User, req);
                response.Success = true;
                response.Message = "Contratante criado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> AtualizarContratante(ContratanteDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await contratanteService.AtualizarContratante(User, req);
                response.Success = true;
                response.Message = "Contratante atualizado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpGet("{contratanteId}")]
        [ProducesDefaultResponseType(typeof(BuscarContratanteResponse))]
        public async Task<IActionResult> BuscarContratante(Guid contratanteId)
        {
            var response = new BuscarContratanteResponse();
            try
            {
                response.Data = await contratanteService.BuscarContratante(User, contratanteId);
                response.Success = true;
                response.Message = "Contratante recuperado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ListarContratanteResponse))]
        public async Task<IActionResult> ListarContratante(ListarContratanteRequest req)
        {
            var response = new ListarContratanteResponse() { PageInfo = req.PageInfo };
            try
            {
                var contratantes = await contratanteService.ListarContratante(User, req);
                response.Success = true;
                response.Message = "Contratantes recuperados com sucesso";
                response.Data = contratantes.ToList();
                response.ItensTotal = contratantes.TotalCount;
                response.Pages = contratantes.TotalPage;
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
