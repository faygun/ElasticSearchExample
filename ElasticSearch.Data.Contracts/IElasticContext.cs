using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch.Data.Contracts
{
    public interface IElasticContext
    {
        IndexResponseDTO CreateIndex<T>(string indexName, string aliasName) where T : class;
        IndexResponseDTO Index<T>(string indexName, T document) where T : class;
        IndexResponseDTO IndexForList<T>(string indexName, List<T> documentList) where T : class;
        SearchResponseDTO<T> Search<T>(ISearchRequest searchRequest) where T : class;
    }
}
