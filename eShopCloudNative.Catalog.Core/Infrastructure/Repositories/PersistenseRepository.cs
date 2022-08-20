using eShopCloudNative.Catalog.Entities;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NH = NHibernate;

namespace eShopCloudNative.Catalog.Infrastructure.Repositories;
internal class PersistenseRepository
{
   
    public PersistenseRepository(ISession session)
    {
        this.Session = session;
    }

    protected NH.ISession Session { get; }


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
