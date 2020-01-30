namespace Dispatching.ReadModel.Mappers
{
    internal interface IApply<T>
    {
        void Apply(T current, T updated);
    }
}
