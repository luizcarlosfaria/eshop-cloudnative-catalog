using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH = NHibernate;


namespace eShopCloudNative.Catalog.Infrastructure.Repositories;
internal abstract class QueryRepository<T>
    where T : class, IBaseEntity
{
    protected QueryRepository(NH.IStatelessSession session)
    {
        this.Session = session;
    }

    protected NH.IStatelessSession Session { get; }

    protected IQueryOver<T, T> QueryOver => this.Session.QueryOver<T>();

    protected IQueryable<T> Query => this.Session.Query<T>();
}
