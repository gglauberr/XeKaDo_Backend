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
    public class ConvidadoController : BaseController
    {
        private readonly IConvidadoService convidadoService;

        public ConvidadoController(
              IConvidadoService convidadoService
        )
        {
            this.convidadoService = convidadoService ?? throw new ArgumentNullException(nameof(convidadoService));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> CriarConvidado(ConvidadoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await convidadoService.CriarConvidado(User, req);
                response.Success = true;
                response.Message = "Convidado criado com sucesso";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> AtualizarConvidado(ConvidadoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await convidadoService.AtualizarConvidado(User, req);
                response.Success = true;
                response.Message = "Convidado atualizado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpGet("{convidadoId}")]
        [ProducesDefaultResponseType(typeof(BuscarConvidadoResponse))]
        public async Task<IActionResult> BuscarConvidado(Guid convidadoId)
        {
            var response = new BuscarConvidadoResponse();
            try
            {
                response.Data = await convidadoService.BuscarConvidado(User, convidadoId);
                response.Success = true;
                response.Message = "Convidado recuperado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ListarConvidadoResponse))]
        public async Task<IActionResult> ListarConvidados(ListarConvidadoRequest req)
        {
            var response = new ListarConvidadoResponse() { PageInfo = req.PageInfo };
            try
            {
                var convidados = await convidadoService.ListarConvidados(User, req);
                response.Success = true;
                response.Message = "Convidados recuperados com sucesso";
                response.Data = convidados.ToList();
                response.ItensTotal = convidados.TotalCount;
                response.Pages = convidados.TotalPage;
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
