namespace Dispatching.Persistence.Mappers
{
    internal interface IMapToDomainModel<TData, TDomain>
    {
        TDomain Map(TData input);
    }
}