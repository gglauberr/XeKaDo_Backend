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
    public class AmbienteController : BaseController
    {
        private readonly IAmbienteService ambienteService;

        public AmbienteController(
              IAmbienteService ambienteService
        )
        {
            this.ambienteService = ambienteService ?? throw new ArgumentNullException(nameof(ambienteService));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> CriarAmbiente(AmbienteDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await ambienteService.CriarAmbiente(User, req);
                response.Success = true;
                response.Message = "Ambiente criado com sucesso.";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> AtualizarAmbiente(AmbienteDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await ambienteService.AtualizarAmbiente(User, req);
                response.Success = true;
                response.Message = "Ambiente atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpGet("{ambienteId}")]
        [ProducesDefaultResponseType(typeof(BuscarAmbienteResponse))]
        public async Task<IActionResult> BuscarAmbiente(Guid ambienteId)
        {
            var response = new BuscarAmbienteResponse();
            try
            {
                response.Data = await ambienteService.BuscarAmbiente(User, ambienteId);
                response.Success = true;
                response.Message = "Ambiente atualizado com sucesso.";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ListarAmbientesResponse))]
        public async Task<IActionResult> ListarAmbientes(ListarAmbienteRequest req)
        {
            var response = new ListarAmbientesResponse() { PageInfo = req.PageInfo };
            try
            {
                var ambientes = await ambienteService.ListarAmbientes(User, req);
                response.Success = true;
                response.Message = "Ambientes recuperados com sucesso";
                response.Data = ambientes.ToList();
                response.ItensTotal = ambientes.TotalCount;
                response.Pages = ambientes.TotalPage;
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
