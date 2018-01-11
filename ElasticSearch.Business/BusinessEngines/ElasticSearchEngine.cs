using ElasticSearch.Business.Contracts;
using ElasticSearch.Data.Contracts;
using Nest;
using System.Collections.Generic;
using System.Linq;

namespace ElasticSearch.Business.BusinessEngines
{
    public class ElasticSearchEngine : IElasticSearchEngine
    {
        private readonly string _indexName;
        private readonly int _size;
        private readonly int _from;
        private readonly List<QueryContainer> _queryContainer;
        private readonly IElasticContext _elasticContext;
        public ElasticSearchEngine(ElasticSearchBuilder builder)
        {
            _indexName = builder.IndexName;
            _size = builder.Size;
            _from = builder.From;
            _queryContainer = builder.ContainerList;
            _elasticContext = builder.ElasticContext;
        }
        public List<T> Execute<T>() where T : class
        {

            //var query = _queryContainer.Cast<QueryContainer>().ToList();
            var response = _elasticContext.Search<T>(new SearchRequest(_indexName, typeof(T))
            {
                Size = _size,
                From = _from,
                Query = new BoolQuery {Should =  _queryContainer, MinimumShouldMatch = new MinimumShouldMatch("1%")} ,
                
                
            });
            
            if (response.IsValid)
            {
                return response.Documents.ToList();
            }

            return null;
        }
    }
}
