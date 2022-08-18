using NH = NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Infrastructure.Repositories;
internal abstract class BaseRepository
{
    public BaseRepository(NH.ISession session)
    {
        this.Session = session;
    }

    protected NH.ISession Session { get; }
}
