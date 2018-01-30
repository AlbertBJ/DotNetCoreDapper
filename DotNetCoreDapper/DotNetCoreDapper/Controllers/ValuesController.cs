using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCore.Business.Interfaces;

namespace DotNetCoreDapper.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private IProductBusiness iProduct;
        public ValuesController(IProductBusiness iProductBusiness)
        {
            this.iProduct = iProductBusiness;
        }
        // GET api/values
        [HttpGet]
        [ApiExplorerSettings(GroupName = "docV1")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ApiExplorerSettings(GroupName = "docV1")]
        public string Get(int id)
        {
            return string.Empty;
            //return iProduct.GetList("SELECT * FROM Product ").ToString(); 
        }

        // POST api/values
        [HttpPost]
        [ApiExplorerSettings(GroupName = "docV2")]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ApiExplorerSettings(GroupName = "docV2")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [ApiExplorerSettings(GroupName = "docV2")]
        public void Delete(int id)
        {
        }
    }
}
