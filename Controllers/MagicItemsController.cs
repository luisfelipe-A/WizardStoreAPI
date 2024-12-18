using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WizStore.Data;
using WizStore.Entities;
using WizStore.Auth;
using Microsoft.EntityFrameworkCore;

namespace WizStore.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MagicItemsController(DataContext context) : ControllerBase
    {
        private DataContext _context = context;

        // GET: api/MagicItems/all
        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetAllItems()
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/lowstock
        [HttpGet("LowStock")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetLowStockItems()
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .Where(mi => mi.Quantity < MagicItem.GetLowStockAmount())
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/favoredlowstock
        //returns any favored as high priority items that are low on stock
        [HttpGet("FavoredLowStock")]
        public async Task<ActionResult<IEnumerable<MagicItem>>> GetLowStockFavoredItems()
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            return await _context.MagicItems
                .OrderByDescending(mi => mi.MagicPower)
                .Where(mi => mi.Quantity < MagicItem.GetLowStockAmount() && mi.IsFavored)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/MagicItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MagicItem>> GetItem(string name)
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            var item = await _context.MagicItems.FindAsync(name);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        // PUT: api/MagicItems/5
        [Authorize(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(string name, MagicItem item)
        {
            if (name != item.Name)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/MagicItems
        [Authorize(Role.Admin)]
        [HttpPost]
        public async Task<ActionResult<MagicItem>> PostItem(MagicItem item)
        {
            if (_context.MagicItems == null)
            {
                return Problem("Entity set 'WizardStoreContext.MagicItems'  is null.");
            }
            _context.MagicItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { name = item.Name }, item);
        }

        // DELETE: api/MagicItems/5
        [Authorize(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(string name)
        {
            if (_context.MagicItems == null)
            {
                return NotFound();
            }
            var item = await _context.MagicItems.FindAsync(name);
            if (item == null)
            {
                return NotFound();
            }

            _context.MagicItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ItemExists(string name)
        {
            return (_context.MagicItems?.Any(e => e.Name == name)).GetValueOrDefault();
        }
    }
}
