using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Controllers
{
    [Route("api/salesInvoiceDetails")]
    [ApiController]
    public class SalesInvoiceDetailsController : ControllerBase
    {
        private readonly SalesContext _context;

        public SalesInvoiceDetailsController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/salesInvoiceDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesInvoiceDetail>>> GetSalesInvoiceDetails()
        {
            return await _context.SalesInvoiceDetails.ToListAsync();
        }

        // GET: api/salesInvoiceDetails/1
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<SalesInvoiceDetail>>> GetSalesInvoiceDetail(int id)
        {
            var salesInvoiceDetails = await _context.SalesInvoiceDetails
                .Where(detail => detail.InvoiceNo == id)
                .ToListAsync();

            if (salesInvoiceDetails == null || !salesInvoiceDetails.Any())
            {
                return NotFound();
            }

            return salesInvoiceDetails;
        }

        // POST: api/salesInvoiceDetails
        [HttpPost]
        public async Task<ActionResult<IEnumerable<SalesInvoiceDetail>>> PostSalesInvoiceDetails(IEnumerable<SalesInvoiceDetail> salesInvoiceDetails)
        {
            // Assuming all details in the list have the same invoice number
            int invoiceNumber = salesInvoiceDetails.FirstOrDefault()?.InvoiceNo ?? 0;

            // Set the invoice number for each detail
            foreach (var detail in salesInvoiceDetails)
            {
                detail.InvoiceNo = invoiceNumber;
            }

            _context.SalesInvoiceDetails.AddRange(salesInvoiceDetails);
            await _context.SaveChangesAsync();

            // Return a 201
            return CreatedAtAction(nameof(GetSalesInvoiceDetails), salesInvoiceDetails);
        }




    }
}