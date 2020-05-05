using Dispatching.Persistence.Mappers;
using DomainDrivenDesign.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dispatching.Persistence
{
    internal abstract class Repository<TDomainModel, TData> where TDomainModel : Entity<TDomainModel>
    {
        private readonly DispatchingDbContext _context;
        private readonly IMapToPersistenceModel<TDomainModel, TData> _domainModelMapper;
        private readonly IMapToDomainModel<TData, TDomainModel> _persistenceModelMapper;

        protected Repository(DispatchingDbContext context,
            IMapToPersistenceModel<TDomainModel, TData> domainModelMapper,
            IMapToDomainModel<TData, TDomainModel> persistenceModelMapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _domainModelMapper = domainModelMapper ?? throw new ArgumentNullException(nameof(domainModelMapper));
            _persistenceModelMapper = persistenceModelMapper ?? throw new ArgumentNullException(nameof(persistenceModelMapper));
        }

        public async Task<TDomainModel> Get(Id<TDomainModel> id)
        {
            var data = await Get(id.ToGuid());
            if (data == null)
            {
                throw new KeyNotFoundException($"Cannot find {typeof(TDomainModel).FullName} with id {id}.");
            }

            return _persistenceModelMapper.Map(data);
        }

        public async Task Update(TDomainModel domainModel)
        {
            var data = await Get(domainModel.Id.ToGuid());

            if (data == null)
            {
                data = _domainModelMapper.CreateNew();
                _domainModelMapper.Apply(domainModel, data);
                await AddAsync(data);
            }
            else
            {
                _domainModelMapper.Apply(domainModel, data);
            }

            await _context.SaveChangesAsync();
        }

        protected abstract Task<TData> Get(Guid id);

        protected abstract Task AddAsync(TData newEntity);
    }
}