namespace Dispatching.Persistence.Mappers
{
    internal interface IMapToPersistenceModel<TDomain, TData>
    {
        void Apply(TDomain domainmodel, TData data);

        TData CreateNew();
    }
}
