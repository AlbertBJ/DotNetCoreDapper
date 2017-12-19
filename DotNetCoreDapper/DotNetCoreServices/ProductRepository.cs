using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Repository.Interfaces;

namespace DotNetCore.Repository
{
    public class ProductRepository : IProductRepository
    {
        public string test()
        {
            return $"test";
        }
    }
}
