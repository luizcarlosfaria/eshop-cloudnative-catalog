using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH = NHibernate;

namespace eShopCloudNative.Catalog.Architecture.Data;
public class CatalogPersistenseRepository<TEntityBase> : PersistenseRepository<TEntityBase>
    where TEntityBase : IEntity
{

    public CatalogPersistenseRepository(ISession session) : base(session) { }

}
