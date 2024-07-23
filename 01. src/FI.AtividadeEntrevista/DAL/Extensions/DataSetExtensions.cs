using System;
using System.Collections.Generic;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Extensions
{
    public static class DataSetExtensions
    {
        public static List<T> Converter<T>(this DataSet ds) where T : new()
        {
            List<T> lista = new List<T>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                var properties = typeof(T).GetProperties();
                var columnNames = GetColumnNames(ds.Tables[0]);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    T obj = new T();
                    foreach (var property in properties)
                    {
                        string propertyName = property.Name;

                        // Procurar o nome da coluna correspondente no DataSet
                        string columnName = columnNames.Find(c => string.Equals(c, propertyName, StringComparison.OrdinalIgnoreCase));
                        if (columnName != null)
                        {
                            object value = row[columnName];

                            if (value != DBNull.Value)
                            {
                                //object convertedValue = ValidateAndConvertValue(value, property.PropertyType);
                                property.SetValue(obj, value);
                            }
                        }
                    }

                    lista.Add(obj);
                }
            }

            return lista;
        }

        private static List<string> GetColumnNames(DataTable table)
        {
            List<string> columnNames = new List<string>();
            foreach (DataColumn column in table.Columns)
            {
                columnNames.Add(column.ColumnName);
            }
            return columnNames;
        }
    }
}
