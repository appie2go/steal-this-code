using Dispatching.ReadModel.Mappers;
using Dispatching.ReadModel.PersistenceModel;
using System;
using System.Threading.Tasks;

namespace Dispatching.ReadModel
{
    internal abstract class Repository<T> where T : Entity, new()
    {
        private readonly DispatchingReadDbContext _dbContext;
        private readonly IApply<T> _mapper;

        protected Repository(DispatchingReadDbContext dbContext, IApply<T> mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Save(T input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            var item = await FindById(input.Id);
            if (item == null) 
            {
                item = new T();
                await Add(item);
            }

            _mapper.Apply(item, input);
            await _dbContext.SaveChangesAsync();
        }

        public abstract Task<T> FindById(Guid id);

        protected abstract Task Add(T newItem);
    }
}
