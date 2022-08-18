using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Infrastructure.Repositories;
internal abstract class QueryRepository<T> : BaseRepository
    where T : class, IBaseEntity
{
    protected QueryRepository(ISession session) : base(session)
    {
    }

    protected IQueryOver<T, T> QueryOver => this.Session.QueryOver<T>();

    protected IQueryable<T> Query => this.Session.Query<T>();
}
