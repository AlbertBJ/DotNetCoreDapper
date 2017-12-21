using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Business.Interfaces;
using DotNetCore.Entities;
using DotNetCore.Repository.Interfaces;
using System.Linq;

namespace DotNetCore.Business
{
    public class ProductBusiness : IProductBusiness
    {
        private IProductRepository iProduct;
        private IBaseRepository<ProductEntity> ibase;
        public ProductBusiness(IProductRepository iProductRepository, IBaseRepository<ProductEntity> ibaseRepository)
        {
            this.iProduct = iProductRepository;
            this.ibase = ibaseRepository;
        }

        public string test()
        {
            return this.iProduct.test();
        }

        public List<ProductEntity> GetList(string sql)
        {
            return ibase.SelectCommond(sql).ToList<ProductEntity>();
        }
    }
}
