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
    public class EventoController : BaseController
    {
        private readonly IEventoService eventoService;

        public EventoController(
              IEventoService eventoService
        )
        {
            this.eventoService = eventoService ?? throw new ArgumentNullException(nameof(eventoService));
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> CriarEvento(EventoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await eventoService.CriarEvento(User, req);
                response.Success = true;
                response.Message = "Evento criado com sucesso";
            }
            catch(Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(BaseResponse<string>))]
        public async Task<IActionResult> AtualizarEvento(EventoDTO req)
        {
            var response = new BaseResponse<string>();
            try
            {
                await eventoService.AtualizarEvento(User, req);
                response.Success = true;
                response.Message = "Evento atualizado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpGet("{eventoId}")]
        [ProducesDefaultResponseType(typeof(BuscarEventoResponse))]
        public async Task<IActionResult> BuscarEvento(Guid eventoId)
        {
            var response = new BuscarEventoResponse();
            try
            {
                response.Data = await eventoService.BuscarEvento(User, eventoId);
                response.Success = true;
                response.Message = "Evento recuperado com sucesso";
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }

        [HttpPost]
        [ProducesDefaultResponseType(typeof(ListarEventosResponse))]
        public async Task<IActionResult> ListarEventos(ListarEventoRequest req)
        {
            var response = new ListarEventosResponse() { PageInfo = req.PageInfo };
            try
            {
                var eventos = await eventoService.ListarEventos(User, req);
                response.Success = true;
                response.Message = "Eventos recuperados com sucesso";
                response.Data = eventos.ToList();
                response.ItensTotal = eventos.TotalCount;
                response.Pages = eventos.TotalPage;
            }
            catch (Exception ex)
            {
                response.MontaErro(ex);
            }
            return Result(response);
        }
    }
}
