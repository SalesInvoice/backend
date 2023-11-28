using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Controllers
{
    [Route("api/salesInvoices")]
    [ApiController]
    public class SalesInvoicesController : Controller
    {
        private readonly SalesContext _context;
    
        public SalesInvoicesController(SalesContext context)
        {
            _context = context;
        }

        //GET: api/salesInvoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesInvoice>>> GetSalesInvoice()
        {
            return await _context.SalesInvoices.ToListAsync();
        }

        //GET: api/salesInvoices/1
        [HttpGet("{id}")]
        public async Task<ActionResult<SalesInvoice>> GetSalesInvoice(int id)
        {
            var salesInvoice = await _context.SalesInvoices.FindAsync(id);

            if (salesInvoice == null)
            {
                return NotFound();
            }

            return salesInvoice;
        }
        // GET: api/salesInvoices/lastInvoiceNumber
        private const int INITIAL_INVOICE_NUMBER = 1;

        [HttpGet("lastInvoiceNumber")]
        public async Task<ActionResult<int>> GetLastInvoiceNumber()
        {
            var lastInvoice = await _context.SalesInvoices
                .OrderByDescending(si => si.InvoiceNo)
                .FirstOrDefaultAsync();

            if (lastInvoice != null)
            {
                return Ok(lastInvoice.InvoiceNo);
            }
            else
            {
                return Ok(INITIAL_INVOICE_NUMBER);
            }
        }


        //POST: api/salesInvoices
        [HttpPost]
        public async Task<ActionResult<SalesInvoice>> PostSalesInvoice(SalesInvoice salesInvoice)
        {
            _context.SalesInvoices.Add(salesInvoice);

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSalesInvoice), new { id = salesInvoice.InvoiceNo }, salesInvoice);
        }

        //PUT: api/salesInvoices/1
        [HttpPut]
        public async Task<ActionResult<SalesInvoice>> PutSalesInvoice(int id , SalesInvoice salesInvoice)
        {
            if (id != salesInvoice.InvoiceNo)
            {
                // Return a 400 Bad Request
                return BadRequest();
            }

            // Update an existing salesInvoice in the database
            _context.Entry(salesInvoice).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return a 204 
            return NoContent();
        }

        // DELETE: api/salesInvoices/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<SalesInvoice>> DeleteItem(int id)
        {
            // Find a item by ID in the database
            var salesInvoice = await _context.SalesInvoices.FindAsync(id);

            if (salesInvoice == null)
            {
                // 404
                return NotFound();
            }

            // Remove the item from the database
            _context.SalesInvoices.Remove(salesInvoice);
            await _context.SaveChangesAsync();

            // 204 ...
            return NoContent();
        }

    }
}