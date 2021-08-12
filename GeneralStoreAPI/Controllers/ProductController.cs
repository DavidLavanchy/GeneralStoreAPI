using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class ProductController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //POST (create)
        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok("Product successfully created!");
            }
            return BadRequest(ModelState);
        }
    }
}
