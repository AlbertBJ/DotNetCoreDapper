using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Repository.Interfaces;
using Microsoft.Extensions.Options;
using DotNetCore.Entities;

namespace DotNetCore.Repository
{
    public class ProductRepository : BaseRepository<ProductEntity>, IProductRepository
    {
        public ProductRepository(IOptions<Option> optionsAccessor) : base(optionsAccessor)
        {

        }
        public string test()
        {
            return "test";
        }
    }
}
