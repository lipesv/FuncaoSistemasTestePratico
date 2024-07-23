using System.Net;

namespace FI.AtividadeEntrevista.CONTRACTS.Validation
{
    public interface IValidationException
    {
        string Message { get; }
        HttpStatusCode StatusCode { get; }
    }
}
