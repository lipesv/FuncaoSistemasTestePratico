using FI.AtividadeEntrevista.BLL.Exceptions;
using FI.AtividadeEntrevista.CONTRACTS.Validation;
using FI.AtividadeEntrevista.DML.Extensions;
using System;
using System.Net;

namespace FI.AtividadeEntrevista.BLL.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidateCep(string cep)
        {
            throw new NotImplementedException();
        }

        public void ValidateCpf(string cpf)
        {
            if (!cpf.EhValido())
            {
                throw new ValidationException("CPF inválido.", HttpStatusCode.BadRequest);
            }
        }
    }
}
