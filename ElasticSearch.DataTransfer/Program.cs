using System;
using ElasticSearch.Core.Common;
using ElasticSearch.Data;
using ElasticSearch.Data.Entities;
using System.Collections.Generic;
using Nest;
using ElasticSearch.Business;

namespace ElasticSearch.DataTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            string indexName = "product_search";
            indexName = "product_search_201801111243";
            //var aliasName = indexName;
            //indexName = string.Concat(indexName, "_", DateTime.Now.ToString("yyyyMMddHHss"));

            var client = new ElasticClient(ElasticHelper.Instance.GetConnectionSettings());
            
            var context = new ElasticContext(client);

            //CreateIndex(context, indexName, aliasName);
            //Index(context, indexName);
            //IndexForList(context, indexName);
            Search(indexName, context);
            Console.ReadKey();
        }
        private static void Search(string indexName, ElasticContext context)
        {
            var searchList = new ElasticSearchBuilder(indexName, context)
                .SetSize(10)
                .SetFrom(0)
                .AddTermQuery("omer", "name")
                //.AddRangeFilter(190, 300, "price")
                .Build()
                .Execute<Product>();

            if (searchList == null)
                return;

            foreach (var item in searchList)
            {
                Console.WriteLine(string.Format("id : {0} Name : {1} Description : {2} Price : {3}", item.Id, item.Name, item.Description, item.Price));
            }
        }

        private static void IndexForList(ElasticContext context, string indexName)
        {
            var response = context.IndexForList(indexName, new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Omer Shoes",
                    Description = "Size is 42, Mark is Vans",
                    Price = 180
                },
                new Product
                {
                    Id = 2,
                    Name = "Atıf Shoes",
                    Description = "Size is 45, Mark is Reebok",
                    Price = 350
                },
                new Product
                {
                    Id = 3,
                    Name = "Babuş Shoes",
                    Description = "Size is 40, Mark is Nike",
                    Price = 250
                },
                new Product
                {
                    Id = 4,
                    Name = "Tennis Shoes",
                    Description = "Size is 41, Mark is Lacoste",
                    Price = 280
                },
                new Product
                {
                    Id = 5,
                    Name = "Football Shoes",
                    Description = "Size is 43, Mark is Adidas",
                    Price = 350
                },
                new Product
                {
                    Id = 6,
                    Name = "Basketball Shoes",
                    Description = "Size is 47, Mark is AndOne",
                    Price = 450
                }
            });

            Console.WriteLine(response.StatusMessage);
        }
        private static void Index(ElasticContext context, string indexName)
        {
            var response = context.Index(indexName, new Product
            {
                Id = 8,
                Name = "Sweatshirt",
                Description = "100% Cotton, Colour is red.",
                Price = 39
            });

            Console.WriteLine(response.StatusMessage);
        }

        private static void CreateIndex(ElasticContext context, string indexName, string aliasName)
        {
            var response = context.CreateIndex<Product>(indexName, aliasName);

            Console.WriteLine(response.StatusMessage);
        }
    }
}
