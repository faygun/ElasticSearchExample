using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Business.Contracts
{
    public interface IElasticSearchEngine
    {
        List<T> Execute<T>() where T : class;
    }
}
