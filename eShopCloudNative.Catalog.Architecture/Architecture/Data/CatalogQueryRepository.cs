using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace eShopCloudNative.Catalog.Architecture.Data;
public abstract class CatalogQueryRepository<TEntityBase> : QueryRepository<TEntityBase>
    where TEntityBase : class, ICatalogEntity
{
    protected CatalogQueryRepository(ISession session) : base(session)
    {
    }
}
