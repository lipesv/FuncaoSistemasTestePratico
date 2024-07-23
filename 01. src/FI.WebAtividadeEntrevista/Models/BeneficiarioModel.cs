using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        [DisplayName(displayName: "CPF do Beneficiário")]
        [Required(ErrorMessage ="{0} é Obrigatório")]
        public string Cpf { get; set; }

        [DisplayName(displayName: "Nome do Beneficiário")]
        [Required(ErrorMessage = "{0} é Obrigatório")]
        public string Nome { get; set; }

        [Required]
        public long IdCliente { get; set; }

    }
}