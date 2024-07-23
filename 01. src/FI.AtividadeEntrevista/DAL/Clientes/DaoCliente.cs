﻿using FI.AtividadeEntrevista.DAL.Extensions;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FI.AtividadeEntrevista.DAL
{
    /// <summary>
    /// Classe de acesso a dados de Cliente
    /// </summary>
    internal class DaoCliente : AcessoDados
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal long Incluir(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Sobrenome", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Nacionalidade", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.Cpf.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Email", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Telefone", cliente.Telefone.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Estado", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Cidade", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("Logradouro", cliente.Logradouro));

            DataSet ds = base.Consultar("FI_SP_IncClienteV2", parametros);
            long ret = 0;

            if (ds.Tables[0].Rows.Count > 0)
                long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);

            return ret;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal DML.Cliente Consultar(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);

            // Verificar se o DataSet tem tabelas e linhas antes de acessar o valor
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Preencher o objeto Cliente a partir do primeiro result set
                Cliente cliente = ds.Tables[0].Rows[0].ToObject<Cliente>();

                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    // Preencher a lista de Beneficiarios a partir do segundo result set
                    cliente.Beneficiarios = ds.Tables[1].AsEnumerable()
                                                        .Select(row => row.ToObject<DML.Beneficiario>())
                                                        .ToList();
                }

                return cliente;
            }

            return null;
        }

        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF.Replace(".", "").Replace("-", "")));

            DataSet ds = base.Consultar("FI_SP_VerificaCliente", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        internal List<DML.Cliente> Pesquisa(int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("iniciarEm", iniciarEm));
            parametros.Add(new System.Data.SqlClient.SqlParameter("quantidade", quantidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("campoOrdenacao", campoOrdenacao));
            parametros.Add(new System.Data.SqlClient.SqlParameter("crescente", crescente));

            DataSet ds = base.Consultar("FI_SP_PesqCliente", parametros);
            List<DML.Cliente> cli = ds.Converter<DML.Cliente>();

            int iQtd = 0;

            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                int.TryParse(ds.Tables[1].Rows[0][0].ToString(), out iQtd);

            qtd = iQtd;

            return cli;
        }

        /// <summary>
        /// Lista todos os clientes
        /// </summary>
        internal List<DML.Cliente> Listar()
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", 0));

            DataSet ds = base.Consultar("FI_SP_ConsCliente", parametros);
            List<DML.Cliente> cli = ds.Converter<DML.Cliente>();

            return cli;
        }

        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Alterar(DML.Cliente cliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", cliente.Id));
            parametros.Add(new System.Data.SqlClient.SqlParameter("NOME", cliente.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("SOBRENOME", cliente.Sobrenome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("NACIONALIDADE", cliente.Nacionalidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", cliente.Cpf.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("EMAIL", cliente.Email));
            parametros.Add(new System.Data.SqlClient.SqlParameter("TELEFONE", cliente.Telefone.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CEP", cliente.CEP.RemoveMascara()));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ESTADO", cliente.Estado));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CIDADE", cliente.Cidade));
            parametros.Add(new System.Data.SqlClient.SqlParameter("LOGRADOURO", cliente.Logradouro));

            base.Executar("FI_SP_AltCliente", parametros);
        }


        /// <summary>
        /// Excluir Cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("FI_SP_DelCliente", parametros);
        }

        //private List<DML.Cliente> Converter(DataSet ds)
        //{
        //    List<DML.Cliente> lista = new List<DML.Cliente>();
        //    if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow row in ds.Tables[0].Rows)
        //        {
        //            DML.Cliente cli = new DML.Cliente();
        //            cli.Id = row.Field<long>("Id");
        //            cli.Nome = row.Field<string>("Nome");
        //            cli.Sobrenome = row.Field<string>("Sobrenome");
        //            cli.Nacionalidade = row.Field<string>("Nacionalidade");
        //            cli.Cpf = row.Field<string>("CPF");
        //            cli.Email = row.Field<string>("Email");
        //            cli.Telefone = row.Field<string>("Telefone");
        //            cli.CEP = row.Field<string>("CEP");
        //            cli.Estado = row.Field<string>("Estado");
        //            cli.Cidade = row.Field<string>("Cidade");
        //            cli.Logradouro = row.Field<string>("Logradouro");

        //            lista.Add(cli);
        //        }
        //    }

        //    return lista;
        //}
    }
}
