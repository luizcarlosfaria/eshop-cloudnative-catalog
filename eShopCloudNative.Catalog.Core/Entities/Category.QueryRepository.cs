using eShopCloudNative.Catalog.Infrastructure.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
internal class CategoryQueryRepository : QueryRepository<Category>
{
    public CategoryQueryRepository(IStatelessSession session) : base(session)
    {
    }

    public async Task<Category> GetCategoryAsync(int categoryId)
        => await this.QueryOver.Where(it => it.CategoryId == categoryId).SingleOrDefaultAsync();


}
