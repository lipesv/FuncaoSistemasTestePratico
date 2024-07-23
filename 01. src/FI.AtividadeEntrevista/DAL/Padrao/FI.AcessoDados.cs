using FI.AtividadeEntrevista.DAL.Padrao;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FI.AtividadeEntrevista.DAL
{
    public class AcessoDados
    {
        private string stringDeConexao
        {
            get
            {
                ConnectionStringSettings conn = ConfigurationManager.ConnectionStrings["BancoDeDados"];
                if (conn != null)
                    return conn.ConnectionString;
                else
                    return string.Empty;
            }
        }

        internal void Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(stringDeConexao);
            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            conexao.Open();
            try
            {
                comando.ExecuteNonQuery();
            }
            finally
            {
                conexao.Close();
            }
        }

        internal DataSet Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(stringDeConexao);

            comando.Connection = conexao;
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataSet ds = new DataSet();
            conexao.Open();

            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }

        public QueryResult GetFilteredRecords(QueryParameters parameters)
        {
            var result = new QueryResult
            {
                Records = new List<Dictionary<string, object>>(),
                TotalRecordCount = 0
            };

            string query = $@"SELECT * FROM 
                            (
                                SELECT ROW_NUMBER() OVER (ORDER BY {parameters.SortColumn}) AS RowNum, *
                                FROM {parameters.TableName}
                                WHERE {parameters.ForeignKeyColumn} = @ForeignKeyValue
                            ) AS RowConstrainedResult
                            WHERE RowNum >= @RowStart AND RowNum < @RowEnd
                            ORDER BY RowNum";

            string countQuery = $"SELECT COUNT(*) FROM {parameters.TableName} WHERE {parameters.ForeignKeyColumn} = @ForeignKeyValue";

            using (var connection = new SqlConnection(stringDeConexao))
            {
                connection.Open();

                // Get total records count
                using (var countCmd = new SqlCommand(countQuery, connection))
                {
                    countCmd.Parameters.AddWithValue("@ForeignKeyValue", parameters.ForeignKeyValue);
                    result.TotalRecordCount = (int)countCmd.ExecuteScalar();
                }

                // Get paginated results
                using (var cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ForeignKeyValue", parameters.ForeignKeyValue);
                    cmd.Parameters.AddWithValue("@RowStart", (parameters.PageIndex - 1) * parameters.PageSize + 1);
                    cmd.Parameters.AddWithValue("@RowEnd", parameters.PageIndex * parameters.PageSize + 1);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                row[reader.GetName(i)] = reader.GetValue(i);
                            }
                            result.Records.Add(row);
                        }
                    }
                }
            }

            return result;
        }
    }
}
