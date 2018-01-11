using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Data.Contracts
{
    public class IndexResponseDTO
    {
        public bool IsValid { get; set; }
        public string StatusMessage { get; set; }
        public Exception Exception { get; set; }
    }
}
