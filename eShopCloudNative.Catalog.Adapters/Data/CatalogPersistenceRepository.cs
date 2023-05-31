using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Architecture.Entities;
using NHibernate;

namespace eShopCloudNative.Catalog.Data;
public class CatalogPersistenceRepository<TEntityBase> : AsyncPersistenceRepository<TEntityBase>
    where TEntityBase : IEntity
{

    public CatalogPersistenceRepository(ISession session) : base(session) { }

}
