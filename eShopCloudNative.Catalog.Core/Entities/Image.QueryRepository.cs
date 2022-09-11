using eShopCloudNative.Architecture.Data.Repositories;
using eShopCloudNative.Catalog.Architecture.Data;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public class ImageQueryRepository : CatalogQueryRepository<Image>
{
    public ImageQueryRepository(ISession session) : base(session)
    {
    }

}
