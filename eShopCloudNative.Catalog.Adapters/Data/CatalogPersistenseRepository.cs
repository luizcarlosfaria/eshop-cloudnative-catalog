using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Entities;
using NHibernate;

namespace eShopCloudNative.Catalog.Data;
public class CatalogPersistenseRepository<TEntityBase> : AsyncPersistenseRepository<TEntityBase>
    where TEntityBase : IEntity
{

    public CatalogPersistenseRepository(ISession session) : base(session) { }

}
