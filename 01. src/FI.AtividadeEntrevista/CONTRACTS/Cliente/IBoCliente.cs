using System.Collections.Generic;

namespace FI.AtividadeEntrevista.CONTRACTS.Cliente
{
    public interface IBoCliente
    {
        void Alterar(DML.Cliente cliente);
        DML.Cliente Consultar(long id);
        void Excluir(long id);
        long Incluir(DML.Cliente cliente);
        List<DML.Cliente> Listar();
        List<DML.Cliente> Pesquisa(int iniciarEm,
                                   int quantidade,
                                   string campoOrdenacao,
                                   bool crescente,
                                   out int qtd);
        
    }
}