using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeKaDo.Domain.Infrastructure
{
    public class XekadoException : Exception
    {
        public int CodResponse { get; private set; } = 500;
        public string Details { get; private set; } = string.Empty;
        public bool Recoverable { get; private set; } = false;

        public XekadoException()
            : this(message: "Ocorreu um erro inesperado ao processar a sua solicitação.") { }

        public XekadoException(string message, Exception innerException = null)
            : this(message, details: string.Empty, innerException) { }


        public XekadoException(string message, int codResponse, Exception innerException = null)
            : this(message, codResponse, details: string.Empty, innerException) { }
        public XekadoException(string message, int codResponse, string details, Exception innerException = null)
            : this(message, codResponse, recoverable: false, details, innerException) { }


        public XekadoException(string message, bool recoverable, Exception innerException = null)
            : this(message, recoverable, string.Empty, innerException) { }

        public XekadoException(string message, bool recoverable, string details, Exception innerException = null)
            : this(message, codResponse: 500, recoverable, details, innerException) { }



        public XekadoException(string message, int codResponse, bool recoverable, string details, Exception innerException = null)
            : this(message, details, innerException)
        {
            CodResponse = codResponse;
            Recoverable = recoverable;
        }

        public XekadoException(string message, string details, Exception innerException = null)
          : base(message ?? "Ocorreu um erro inesperado ao processar a sua solicitação.", innerException)
        { Details = details; }
    }
}
