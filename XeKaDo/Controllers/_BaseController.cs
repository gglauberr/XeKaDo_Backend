using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XeKaDo.Domain.DTO;
using XeKaDo.Domain.Response;

namespace XeKaDo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Produces("application/json", additionalContentTypes:"application/xml")]
    [ProducesErrorResponseType(typeof(BaseResponse))]
    public class BaseController : ControllerBase
    {
        protected IActionResult Result(BaseResponse response)
        {
            if(response is null)
            {
                return BadRequest(new BaseResponse<string>()
                {
                    Message = "Não foi possível montar os resultados para a sua requisição",
                    ErrorDetails = "API Controller error. No Response"
                });
            }

            return (response.Success ? Ok(response) : BadRequest(response) as IActionResult);
        }
    }
}
