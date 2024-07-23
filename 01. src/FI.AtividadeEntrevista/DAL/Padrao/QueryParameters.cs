using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.DAL.Padrao
{
    public class QueryParameters
    {
        public string TableName { get; set; }
        public string ForeignKeyColumn { get; set; }
        public string ForeignKeyValue { get; set; }
        public string SortColumn { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class QueryResult
    {
        public List<Dictionary<string, object>> Records { get; set; }
        public int TotalRecordCount { get; set; }
    }
}
