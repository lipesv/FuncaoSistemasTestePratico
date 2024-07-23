namespace FI.AtividadeEntrevista.CONTRACTS.Validation
{
    public interface IValidationService
    {
        void ValidateCpf(string cpf);
        void ValidateCep(string cep);
    }
}
