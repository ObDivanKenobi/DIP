using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSmells.Models
{
    public class QueryResult
    {
        public bool Succeed { get; private set; }
        public List<string> Errors { get; private set; }

        public QueryResult()
        {
            Succeed = false;
            Errors = new List<string>();
        }

        public QueryResult(bool succeed, List<string> errors)
        {
            Succeed = succeed;
            Errors = errors;
        }
    }
}
