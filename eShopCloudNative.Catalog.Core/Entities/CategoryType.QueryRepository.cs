using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Architecture.Data;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public class CategoryTypeQueryRepository : CatalogQueryRepository<CategoryType>
{
    public CategoryTypeQueryRepository(ISession session) : base(session)
    {
    }

}
