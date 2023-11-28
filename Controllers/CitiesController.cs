using SalesInvoiceProcess.Data;
using SalesInvoiceProcess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class CitiesController : ControllerBase
{
    private readonly SalesContext _context;

    public CitiesController(SalesContext context)
    {
        _context = context;
    }

    // GET: api/cities To get the data from the database
    [HttpGet]
    public async Task<ActionResult<IEnumerable<City>>> GetCities()
    {
        return await _context.Cities.ToListAsync();
    }

    // GET: api/cities/bycountrycode/{countryCode}
    [HttpGet("bycountrycode/{countryCode}")]
    public async Task<ActionResult<IEnumerable<City>>> GetCitiesByCountryCode(int countryCode)
    {
        var cities = await _context.Cities
            .Where(city => city.CountryCode == countryCode)
            .ToListAsync();

        if (cities == null || cities.Count == 0)
        {
            return NotFound();
        }

        return cities;
    }

// GET: api/cities/name
[HttpGet("name/{name}")]
    public async Task<ActionResult<City>> GetCityByName(string name)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.CityEnglishName.ToLower() == name.ToLower());

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<City>> GetCityByCode(int code)
    {
        var city = await _context.Cities
            .FirstOrDefaultAsync(c => c.CityCode == code);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    // GET: api/cities/1   To get specific city the data from the database
    [HttpGet("{id}")]
    public async Task<ActionResult<City>> GetCity(int id)
    {
        var city = await _context.Cities.FindAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        return city;
    }

    // POST: api/cities To add data to the database
    [HttpPost]
    public async Task<ActionResult<City>> PostCity(City city)
    {
        _context.Cities.Add(city);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCity), new { id = city.CityCode }, city);
    }

    // PUT: api/cities/1  To update specific data in the database
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCity(int id, City city)
    {
        if (id != city.CityCode)
        {
            return BadRequest();
        }

        _context.Entry(city).State = EntityState.Modified;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/cities/1    To Delete specific data from the database
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCity(int id)
    {
        var city = await _context.Cities.FindAsync(id);

        if (city == null)
        {
            return NotFound();
        }

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}