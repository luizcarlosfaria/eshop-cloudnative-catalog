using NHibernate;
using NHibernate.Criterion;
using NHibernate.Criterion.Lambda;
using NHibernate.Hql;
using NHibernate.Hql.Ast;
using NHibernate.Hql.Util;
using NHibernate.Linq;
using NHibernate.Action;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;
using eShopCloudNative.Architecture.Data.Repositories;

namespace eShopCloudNative.Catalog.Entities;

public interface ICategoryQueryRepository
{
    public Task<IList<Category>> GetHomeCatalog();

    public Task<IList<Category>> GetCategoriesForMenu();
}
