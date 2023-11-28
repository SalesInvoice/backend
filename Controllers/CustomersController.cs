using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Controllers
{
    // Controller for managing customer-related actions
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Database context for interacting with the sales database
        private readonly SalesContext _context;

        // Constructor for initializing the controller with a SalesContext
        public CustomersController(SalesContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            // Retrieve a list of all customers from the database
            return await _context.Customers.ToListAsync();
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            // Find a customer by ID in the database
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                // Return 404
                return NotFound();
            }

            // Return the found customer
            return customer;
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            // Add a new customer to the database
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Return a 201 Created response
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerCode }, customer);
        }

        // PUT: api/customers/1
        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerCode)
            {
                // Return a 400 Bad Request
                return BadRequest();
            }

            // Update an existing customer in the database
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // Return a 204 No Content
            return NoContent();
        }

        // DELETE: api/customers/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            // Find a customer by ID in the database
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                // 404
                return NotFound();
            }

            // Remove the customer from the database
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            // 204
            return NoContent();
        }
    }
}