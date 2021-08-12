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
    public class TransactionController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        //create a transaction
        [HttpPost]
        public async Task<IHttpActionResult> CreateATransaction(Transaction transaction)
        {
            if (transaction.ProductSKU.IsInStock == false)
            {
                return BadRequest("Product is not in stock");
            }
            if (transaction.ItemCount > transaction.ProductSKU.NumberOfInventory)
            {
                return BadRequest($"There are only {transaction.ProductSKU.NumberOfInventory} in stock");
            }
            if (transaction == null)
            {
                return BadRequest(ModelState);
            }
            _context.Transactions.Add(transaction);
            transaction.ProductSKU.NumberOfInventory = transaction.ProductSKU.NumberOfInventory - transaction.ItemCount;
            await _context.SaveChangesAsync();
            return Ok("Transaction successfully added!");
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();

            return Ok(transactions);
        }
        [HttpPatch]
        public async Task<IHttpActionResult> GetTransactionByID([FromUri] int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return BadRequest(ModelState);
            }

            return Ok(transaction);
        }

        //Bonus

        //Get Transactions By Customer ID
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerTransactionsByID([FromUri] int id)
        {
            List<Transaction> transactions = new List<Transaction>();

            foreach (var transaction in _context.Transactions)
            {
                if (id == transaction.Customer.Id)
                {
                    transactions.Add(transaction);
                }
            }
            await _context.SaveChangesAsync();

            return Ok(transactions);
        }

        //Get Transactions between data range
        [HttpGet]
        public async Task<IHttpActionResult> GetTransactionsByDate([FromUri] DateTime date1, [FromUri] DateTime date2)
        {
            List<Transaction> _transactions = new List<Transaction>();

            foreach (var transaction in _context.Transactions)
            {
                if ((transaction.DateOfTransaction >= date1) && (transaction.DateOfTransaction < date2))
                {
                    _transactions.Add(transaction);
                }

            }

            await Task.Delay(20);

            return Ok(_transactions);
        }
        
        //Total sales by Product SKU
        [HttpGet]
        public async Task<IHttpActionResult> GetSalesBySKU([FromUri] int SKU)
        {
            List<double> _list = new List<double>();

            foreach (var transaction in _context.Transactions)
            {
                
                if (transaction.ProductSKU.SKU == SKU)
                {
                    int itemCount = Convert.ToInt32(transaction.ItemCount);
                    double itemCountDouble = Convert.ToDouble(itemCount);
                    double totalCost = +(transaction.ProductSKU.Cost * itemCountDouble);
                    _list.Add(totalCost);
                }
                
            }

            double sales = _list.Sum();
            await _context.SaveChangesAsync();
            return Ok($"Total sales for Product Sku{SKU} are {sales}");
        }

    }

}

