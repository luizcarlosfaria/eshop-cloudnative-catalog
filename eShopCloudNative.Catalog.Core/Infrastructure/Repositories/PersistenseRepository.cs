using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Infrastructure.Repositories;
internal class PersistenseRepository : BaseRepository
{
    public PersistenseRepository(ISession session) : base(session)
    {
    }


    public async Task SaveAsync(IBaseEntity entity)
    { 
        await this.Session.SaveAsync(entity);
    }

    public async Task UpdateAsync(IBaseEntity entity)
    {
        await this.Session.UpdateAsync(entity);
    }

    public async Task DeleteAsync(IBaseEntity entity)
    {
        await this.Session.DeleteAsync(entity);
    }

}
