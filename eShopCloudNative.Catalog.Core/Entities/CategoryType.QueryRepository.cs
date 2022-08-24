using eShopCloudNative.Catalog.Infrastructure.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
internal class CategoryTypeQueryRepository : QueryRepository<CategoryType>
{
    public CategoryTypeQueryRepository(IStatelessSession session) : base(session)
    {
    }

    public async Task<CategoryType> GetCategoryAsync(int categoryTypeId)
        => await this.QueryOver.Where(it => it.CategoryTypeId == categoryTypeId).SingleOrDefaultAsync();


}
