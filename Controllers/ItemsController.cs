using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly SalesContext _context;

        public ItemsController(SalesContext context)
        {
            _context = context;
        }
        // GET: api/items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetItem()
        {
            return await _context.Items.ToListAsync();
        }

        // GET: api/items/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST: api/items
        [HttpPost]
        public async Task<ActionResult<Item>> PostItem(Item item)
        {
            // Add a new item to the database
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            // Return a 201
            return CreatedAtAction(nameof(GetItem), new { id = item.ItemCode }, item);
        }

        // PUT: api/items/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Item>> PutItem(int id, Item item)
        {
            if (id != item.ItemCode)
            {
                // Return a 400 Bad Request
                return BadRequest();
            }

            // Update an existing item in the database
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return a 204 No Content response
            return NoContent();
        }

        // DELETE: api/items/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Item>> DeleteItem(int id)
        {
            // Find a item by ID in the database
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                // 404
                return NotFound();
            }

            // Remove the item from the database
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            // 204
            return NoContent();
        }

    }
}
