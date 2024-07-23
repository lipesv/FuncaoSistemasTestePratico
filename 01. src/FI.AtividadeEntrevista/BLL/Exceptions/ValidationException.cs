using FI.AtividadeEntrevista.CONTRACTS.Validation;
using System;
using System.Net;

namespace FI.AtividadeEntrevista.BLL.Exceptions
{
    public class ValidationException : Exception, IValidationException
    {
        public HttpStatusCode StatusCode { get; private set; }

        public ValidationException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
