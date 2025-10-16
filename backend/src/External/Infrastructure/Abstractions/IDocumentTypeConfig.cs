using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elastic.Clients.Elasticsearch.Mapping;
using Infrastructure.Docuemnts;

namespace Infrastructure.Abstractions
{
    public interface IDocumentTypeConfig<T> where T : class
    {
        public int Version { get; }
        public string IndexName { get; }
        public string IndexVersionedName => $"{IndexName}_v{Version}";
        public TypeMappingDescriptor<T> ConfigureIndex(TypeMappingDescriptor<T> map);
    }
}