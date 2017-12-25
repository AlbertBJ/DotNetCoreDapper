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
        /// <summary>
        /// 调整 参数由 Ioption 变换为 IoptionSnapshot: Ioption单例模式，在 配置发生变化后，必须重启应用。而IOptionsSnapshot自动更新配置
        /// </summary>
        /// <param name="optionsAccessor"></param>
        public ProductRepository(IOptionsSnapshot<Option> optionsAccessor) : base(optionsAccessor)
        {

        }
        public string test()
        {
            return "test";
        }
    }
}
