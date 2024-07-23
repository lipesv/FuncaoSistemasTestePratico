using FI.AtividadeEntrevista.BLL.Cliente;
using FI.AtividadeEntrevista.DAL.Extensions;
using FI.AtividadeEntrevista.DAL.Padrao;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL.Beneficiario
{
    internal class DaoBeneficiario : AcessoDados
    {
        internal List<DML.Beneficiario> Consultar(long? id, long? IdCliente = null)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            if (id.HasValue)
                parametros.Add(new SqlParameter("ID", id));

            if (IdCliente.HasValue)
                parametros.Add(new SqlParameter("IDCLIENTE", IdCliente));

            DataSet ds = base.Consultar("FI_SP_ConsBenef", parametros);
            List<DML.Beneficiario> beneficiarios = ds.Converter<DML.Beneficiario>();

            return beneficiarios;
        }

        internal bool VerificarExistencia(string CPF, long clienteId)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", clienteId));

            DataSet ds = base.Consultar("FI_SP_VerificaBeneficiarioCliente", parametros);

            return ds != null
                   && ds.Tables.Count > 0
                   && ds.Tables[0].Rows.Count > 0
                   && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0;
        }

        /// <summary>
        /// Inclui um novo beneficiário
        /// </summary>
        /// <param name="beneficiario"></param>
        /// <returns></returns>
        internal long Incluir(DML.Beneficiario beneficiario)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.Cpf.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IdCliente));


            DataSet ds = base.Consultar("FI_SP_IncBeneficiario", parametros);
            long ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        internal QueryResult Listar(QueryParameters parameters)
        {
            return base.GetFilteredRecords(parameters);
        }

        internal void Alterar(DML.Beneficiario beneficiario)
        {
            List<SqlParameter> parametros = new List<SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", beneficiario.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", beneficiario.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.Cpf.RemoveMascara()));

            base.Executar("FI_SP_AltBenef", parametros);
        }
    }
}
