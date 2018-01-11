using ElasticSearch.Business.BusinessEngines;
using ElasticSearch.Data.Contracts;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Business
{
    public class ElasticSearchBuilder
    {
        internal string IndexName;
        internal int Size;
        internal int From;
        internal List<QueryContainer> ContainerList;
        internal IElasticContext ElasticContext;

        public ElasticSearchBuilder(string indexName, IElasticContext context)
        {
            IndexName = indexName;
            ElasticContext = context;

            ContainerList = new List<QueryContainer>();
        }

        public ElasticSearchBuilder SetSize(int size)
        {
            Size = size;

            return this;
        }

        public ElasticSearchBuilder SetFrom(int from)
        {
            From = from;

            return this;
        }

        public ElasticSearchBuilder AddTermQuery(string term, string field)
        {
            var termQuery = new TermQuery()
            {
                Field = new Field(field),
                Value = term
            };

            ContainerList.Add(termQuery);

            return this;
        }
        public ElasticSearchBuilder AddRangeFilter(double gte, double lte, string field)
        {
            var rangeQuery = new NumericRangeQuery
            {
                Field = new Field(field),
                GreaterThanOrEqualTo = gte,
                LessThanOrEqualTo = lte
            };

            ContainerList.Add(rangeQuery);
            
            return this;
        }
        public ElasticSearchEngine Build()
        {
            return new ElasticSearchEngine(this);
        }
    }
}
