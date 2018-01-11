using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch.Core.Common
{
    public class ElasticHelper
    {
        private static readonly Lazy<ElasticHelper> _Instance = new Lazy<ElasticHelper>(() => new ElasticHelper());
        private ElasticHelper()
        {

        }

        public static ElasticHelper Instance
        {
            get
            {
                return _Instance.Value;
            }
        }

        public ConnectionSettings GetConnectionSettings()
        {
            var connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200"));

            return connectionSettings;
        }
    }
}
