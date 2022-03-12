using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XeKaDo.Domain.Infrastructure;

namespace XeKaDo.Domain.Response
{
    public abstract class BaseResponse
    {
        public virtual object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorDetails { get; set; } = "";

        public BaseResponse(bool success = false, string message = "", object data = null)
        {
            Data = data;
            Success = success;
            Message = message;
        }
    }

    public class BaseResponse<TData> : BaseResponse
        where TData : class
    {
        public new TData Data { get; set; }

        public BaseResponse(bool success = false, string message = "", TData data = null) : base(success, message, data) { }
    }

    public class BasePagedResponse<TData> : BaseResponse
        where TData : class
    {
        public PageInfo PageInfo { get; set; }
        public int Pages { get; set; }
        public int ItensTotal { get; set; }
        public bool HasMore { get { return PageInfo.PageNumber < Pages; } }
        public virtual new IEnumerable<TData> Data { get; set; }

        public BasePagedResponse(
            bool success = false,
            string message = "",
            PageInfo pageInfo = new PageInfo(),
            int pages = 0,
            int itensTotal = 0,
            IEnumerable<TData> data = null
        ) : base(success, message)
        {
            Success = success;
            Message = message;
            PageInfo = pageInfo;
            Pages = pages;
            ItensTotal = itensTotal;

            if(data is null)
            {
                Data = new HashSet<TData>();
                ItensTotal = 0;
                Pages = 0;
            }
        }
    }

    public static class BaseResponseExtension
    {
        public static void MontaErro(this BaseResponse response, Exception ex, string genericMessage = "Houve um erro ao realizar a requisição")
        {
            response.Success = false;
            response.Data = null;

            if(ex is XekadoException)
            {
                response.Message = ex.Message;
                response.ErrorDetails = ex.InnerException?.Message ?? "";
            }
            else
            {
                response.Message = genericMessage;
                response.ErrorDetails = ex.Message;
            }
        }
    }
}
