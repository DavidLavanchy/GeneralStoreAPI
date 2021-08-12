using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
<<<<<<< HEAD
        public async Task<IHttpActionResult> CreateProduct([FromBody] Product product)
=======
        public async Task<IHttpActionResult> CreateProduct([FromBody]Product product)
>>>>>>> 3ac21b19f959ee7654298b6ff073f0cdd648f677
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return Ok("Product successfully created!");
            }
            return BadRequest(ModelState);
        }

        //Get All Products
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            List<Product> _listOfProducts = await _context.Products.ToListAsync();

            return Ok(_listOfProducts);
        }

        //GetBySKU
        [HttpGet]
        public async Task<IHttpActionResult> GetProductBySku([FromUri] string sku)
        {
            Product product = await _context.Products.FindAsync(sku);

<<<<<<< HEAD
            if (product == null)
=======
            if(product == null)
>>>>>>> 3ac21b19f959ee7654298b6ff073f0cdd648f677
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }
<<<<<<< HEAD
=======

>>>>>>> 3ac21b19f959ee7654298b6ff073f0cdd648f677
    }
}
