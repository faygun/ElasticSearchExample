using System;
using System.Collections.Generic;
using ElasticSearch.Data.Contracts;
using Nest;
using ElasticSearch.Data.Entities;

namespace ElasticSearch.Data
{
    public class ElasticContext : IElasticContext
    {
        private readonly ElasticClient _elasticClient;
        public ElasticContext(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public IndexResponseDTO IndexForList<T>(string indexName, List<T> documentList) where T : class
        {
            var response = _elasticClient.IndexMany(documentList, indexName);

            return new IndexResponseDTO()
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }
        public IndexResponseDTO CreateIndex<T>(string indexName, string aliasName) where T : class
        {
            var createIndexDescriptor = new CreateIndexDescriptor(indexName)
                .Settings(ci => ci
                    .Analysis(anly => anly
                        .TokenFilters(filter => filter
                            .Stop("turkish_stop", s => s
                                .StopWords("_turkish_")
                            )
                            .Lowercase("turkish_lowercase", lc => lc.Language("turkish"))
                                .AsciiFolding("folding-preserve", ft => ft
                                    .PreserveOriginal()
                            )
                            //.KeywordMarker("turkish_keywords", km => km
                            //    .Keywords("['örnek']")
                            //)
                            .Stemmer("turkish_stemmer", s=> s
                                .Language("turkish")
                            )
                            .AsciiFolding("ascii_folding", af => af
                                .PreserveOriginal(true)
                            )
                        )
                        .Analyzers(anlyz => anlyz
                            .Custom("turkish", c=> c
                                .Tokenizer("standard")
                                .Filters("apostrophe", "turkish_lowercase", "turkish_stop", "turkish_stemmer", "ascii_folding")
                            )
                        )
                    )
                )
                .Mappings(ms => ms
                    .Map<T>(m => m.AutoMap())
                )
                .Aliases(a => a.Alias(aliasName));

            var response = _elasticClient.CreateIndex(createIndexDescriptor);

            return new IndexResponseDTO
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public IndexResponseDTO Index<T>(string indexName, T document) where T : class
        {
            var response = _elasticClient.Index(document, i => i
                            .Index(indexName)
                            .Type<T>());

            return new IndexResponseDTO
            {
                IsValid = response.IsValid,
                StatusMessage = response.DebugInformation,
                Exception = response.OriginalException
            };
        }

        public SearchResponseDTO<T> Search<T>(ISearchRequest searchRequest) where T : class
        {
            var response = _elasticClient.Search<T>(searchRequest);

            return new SearchResponseDTO<T>
            {
                IsValid = response.IsValid,
                Exception = response.OriginalException,
                StatusMessage = response.DebugInformation,
                Documents = response.Documents
            };
        }
    }
}
