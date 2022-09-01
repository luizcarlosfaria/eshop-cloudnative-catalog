using eShopCloudNative.Catalog.Architecture.Data.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
internal class ImageQueryRepository : QueryRepository<Image>
{
    public ImageQueryRepository(IStatelessSession session) : base(session)
    {
    }

}
