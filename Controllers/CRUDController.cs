using EstateAgency.Data;
using EstateAgency.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Xml;

namespace EstateAgency.Controllers
{
    [ApiController]
    [Route("/getAPI/[controller]")]
    public class RealEstateCRUDController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        // GET: /real-estate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstate>>> GetAll()
        {
            return Ok(await _context.RealEstates
                .Include(r => r.District)
                .Include(r => r.EstateType)
                .Include(r => r.BuildingMaterial)

                .ToListAsync());
        }

        // POST: /real-estate
        [HttpPost]
        public async Task<ActionResult<RealEstate>> Create(RealEstate realEstate)
        {
            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = realEstate.EstateCode }, realEstate);
        }

        // PUT: /real-estate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, RealEstate updatedRealEstate)
        {
            if (id != updatedRealEstate.EstateCode)
            {
                return BadRequest();
            }

            _context.Entry(updatedRealEstate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // GET: /real-estate/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstate>> GetById(int id)
        {
            var realEstate = await _context.RealEstates
                .Include(r => r.District)
                .Include(r => r.EstateType)
                .Include(r => r.BuildingMaterial)
                .FirstOrDefaultAsync(m => m.EstateCode == id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return Ok(realEstate);
        }

        // DELETE: /real-estate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RealEstateExists(int id)
        {
            return _context.RealEstates.Any(e => e.EstateCode == id);
        }

    }
    
}
