using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DML
{
    public class Beneficiario
    {
        /// <summary>
        /// Id
        /// </summary>
        public long Id { get; set; }
        
        /// <summary>
        /// Cpf
        /// </summary>  
        public string Cpf { get; set; }
        
        /// <summary>
        /// Nome
        /// </summary>
        public string Nome { get; set; }
        
        /// <summary>
        /// ClienteId
        /// </summary>
        public long IdCliente { get; set; }
    }
}
