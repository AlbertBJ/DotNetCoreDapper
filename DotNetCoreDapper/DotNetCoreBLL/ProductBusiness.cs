using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Business.Interfaces;
using DotNetCore.Repository.Interfaces;

namespace DotNetCore.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository iProduct;
        public ProductBusiness(IProductRepository iProductRepository)
        {
            this.iProduct = iProductRepository;
        }

        public string test()
        {
            return this.iProduct.test();
        }

    }
}
