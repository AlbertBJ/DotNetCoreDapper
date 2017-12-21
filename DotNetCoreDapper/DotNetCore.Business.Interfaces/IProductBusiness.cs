using System;
using System.Collections.Generic;
using System.Text;
using DotNetCore.Entities;

namespace DotNetCore.Business.Interfaces
{
    public interface IProductBusiness
    {
        string test();

        List<ProductEntity> GetList(string sql);
    }
}
