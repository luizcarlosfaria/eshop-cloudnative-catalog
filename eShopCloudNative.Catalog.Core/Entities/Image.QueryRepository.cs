using eShopCloudNative.Catalog.Architecture.Data.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Entities;
public class ImageQueryRepository : QueryRepository<Image>
{
    public ImageQueryRepository(ISession session) : base(session)
    {
    }

}
