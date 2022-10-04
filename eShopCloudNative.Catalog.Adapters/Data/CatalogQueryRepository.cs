using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Entities;
using NHibernate;

namespace eShopCloudNative.Catalog.Data;
public abstract class CatalogQueryRepository<TEntityBase> : QueryRepository<TEntityBase>
    where TEntityBase : class, ICatalogEntity
{
    protected CatalogQueryRepository(ISession session) : base(session)
    {
    }
}
