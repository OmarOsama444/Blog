using Elastic.Clients.Elasticsearch.Mapping;

namespace Infrastructure.Abstractions
{
    public interface IDocumentTypeConfig<T> where T : class
    {
        public int Version { get; }
        public string IndexName { get; }
        public string IndexVersionedName { get; }
        public TypeMappingDescriptor<T> ConfigureIndex(TypeMappingDescriptor<T> map);
    }
}