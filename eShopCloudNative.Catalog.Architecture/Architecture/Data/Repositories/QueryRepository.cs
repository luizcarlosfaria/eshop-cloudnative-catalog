using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH = NHibernate;


namespace eShopCloudNative.Catalog.Architecture.Data.Repositories;
public abstract class QueryRepository<T>
    where T : class, ICatalogBaseEntity
{
    protected QueryRepository(ISession session)
    {
        this.Session = session;
    }

    protected ISession Session { get; }

    protected IQueryOver<T, T> QueryOver => this.Session.QueryOver<T>();

    protected IQueryable<T> Query => this.Session.Query<T>();
    protected ICriteria Criteria => this.Session.CreateCriteria<T>();
}
