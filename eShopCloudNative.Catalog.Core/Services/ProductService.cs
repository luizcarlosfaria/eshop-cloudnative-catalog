using AutoMapper;
using eShopCloudNative.Catalog.Dto;
using eShopCloudNative.Catalog.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopCloudNative.Catalog.Services;
public class ProductService : IProductService
{
    private readonly IMapper mapper;

    public ProductService(IMapper mapper)
    {
        this.mapper = mapper;
    }


}
