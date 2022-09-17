using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;

public abstract class BaseService
{
    protected IMapper Mapper { get; private set; }

    public BaseService(IMapper mapper)
    {
        this.Mapper = mapper;
    }

    protected async Task<IEnumerable<TDto>> ExecuteAndAdaptAsync<TDto, TEntity>(Func<Task<IList<TEntity>>> func)
    {
        var dbResult = await func();

        var apiResult = this.Mapper.Map<IEnumerable<TDto>>(dbResult);

        return apiResult;
    }

    protected IEnumerable<TDto> ExecuteAndAdapt<TDto, TEntity>(Func<IList<TEntity>> func)
    {
        var dbResult = func();

        var apiResult = this.Mapper.Map<IEnumerable<TDto>>(dbResult);

        return apiResult;
    }

    protected async Task<TDto> ExecuteAndAdaptAsync<TDto, TEntity>(Func<Task<TEntity>> func)
    {
        var dbResult = await func();

        var apiResult = this.Mapper.Map<TDto>(dbResult);

        return apiResult;
    }

    protected TDto ExecuteAndAdapt<TDto, TEntity>(Func<TEntity> func)
    {
        var dbResult = func();

        var apiResult = this.Mapper.Map<TDto>(dbResult);

        return apiResult;
    }
}

