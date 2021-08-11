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
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        //Post
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer([FromBody]Customer customer)
        {
            if (ModelState.IsValid)
            {
                //store model in database
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok("Customer was created!");
            }
            // if not, reject it
            return BadRequest(ModelState);
        }
        //get all customers
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();

            return Ok(customers);
        }
        //get customer by id
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
           
        }
    }
}
