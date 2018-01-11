using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Data.Contracts
{
    public class SearchResponseDTO<T>:IndexResponseDTO
    {
        public IEnumerable<T> Documents { get; set; }
    }
}
