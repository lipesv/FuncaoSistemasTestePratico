using FI.AtividadeEntrevista.DAL.Padrao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.CONTRACTS.Beneficiario
{
    public interface IBoBeneficiario
    {
        List<DML.Beneficiario> Consultar(long? id, long? IdCliente = null);

        List<DML.Beneficiario> Listar(int iniciarEm,
                                      int quantidade,
                                      string campoOrdenacao,
                                      bool crescente,
                                      out int qtd,
                                      long clienteId = 0);

        QueryResult Listar(QueryParameters queryParameters);

        long Incluir(DML.Beneficiario beneficiario);

        void Alterar(DML.Beneficiario beneficiario);

        void Excluir(long id);
    }
}
