using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly SalesContext _context;

    public CountriesController(SalesContext context)
    {
        _context = context;
    }

    // GET: api/countries  To get all data country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        return await _context.Countries.ToListAsync();
    }
    // GET: api/countries/names   To get all country names
    [HttpGet("names")]
    public async Task<ActionResult<IEnumerable<string>>> GetCountryNames()
    {
        var countryNames = await _context.Countries
            .Select(country => country.CountryEnglishName)
            .ToListAsync();

        return countryNames;
    }

    // GET: api/countries/name
    [HttpGet("name/{name}")]
    public async Task<ActionResult<Country>> GetCountryByName(string name)
    {
        var country = await _context.Countries
            .FirstOrDefaultAsync(c => c.CountryEnglishName.ToLower() == name.ToLower());

        if (country == null)
        {
            return NotFound();
        }

        return country;
    }

    // GET: api/countries/1    To get specific country
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        return country;
    }

    // POST: api/countries
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCountry), new { id = country.CountryCode }, country);
    }

    // PUT: api/countries/1
 [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        if (id != country.CountryCode)
        {
            return BadRequest();
        }

        _context.Entry(country).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/countries/1
   [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);

        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}