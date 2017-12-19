using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DotNetCore.Entities
{
    /// <summary>
    /// 商品实体
    /// </summary>
    public class ProductEntity
    {
        /// <summary>
        /// 商品ID
        /// </summary>
        [Range(1, int.MaxValue)]
        public int ID { get; set; }

        /// <summary>
		/// 名称
		/// </summary>
		[StringLength(20)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [StringLength(100)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// 商品类别
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Category { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [Range(1, int.MaxValue)]
        public int Count { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [Required]
        public decimal Price { get; set; }
       
    }
}
