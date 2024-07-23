using System;
using System.Data;

namespace FI.AtividadeEntrevista.DAL.Extensions
{
    public static class DataRowExtensions
    {
        public static T ToObject<T>(this DataRow row) where T : new()
        {
            T obj = new T();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.CanWrite && row.Table.Columns.Contains(prop.Name))
                {
                    var value = row[prop.Name];
                    prop.SetValue(obj, value == DBNull.Value ? null : value);
                }
            }
            return obj;
        }
    }

}
