using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RiosAWDB.Data;
using RiosAWDB.Models;

namespace RiosAWDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeePayHistoriesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public EmployeePayHistoriesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/EmployeePayHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeePayHistory>>> GetEmployeePayHistories()
        {
          if (_context.EmployeePayHistories == null)
          {
              return NotFound();
          }
            return await _context.EmployeePayHistories.ToListAsync();
        }

        // GET: api/EmployeePayHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeePayHistory>> GetEmployeePayHistory(int id)
        {
          if (_context.EmployeePayHistories == null)
          {
              return NotFound();
          }
            var employeePayHistory = await _context.EmployeePayHistories.FindAsync(id);

            if (employeePayHistory == null)
            {
                return NotFound();
            }

            return employeePayHistory;
        }

        // PUT: api/EmployeePayHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeePayHistory(int id, EmployeePayHistory employeePayHistory)
        {
            if (id != employeePayHistory.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(employeePayHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeePayHistoryExists(id))
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

        // POST: api/EmployeePayHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeePayHistory>> PostEmployeePayHistory(EmployeePayHistory employeePayHistory)
        {
          if (_context.EmployeePayHistories == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.EmployeePayHistories'  is null.");
          }
            _context.EmployeePayHistories.Add(employeePayHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeePayHistoryExists(employeePayHistory.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeePayHistory", new { id = employeePayHistory.BusinessEntityId }, employeePayHistory);
        }

        // DELETE: api/EmployeePayHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeePayHistory(int id)
        {
            if (_context.EmployeePayHistories == null)
            {
                return NotFound();
            }
            var employeePayHistory = await _context.EmployeePayHistories.FindAsync(id);
            if (employeePayHistory == null)
            {
                return NotFound();
            }

            _context.EmployeePayHistories.Remove(employeePayHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeePayHistoryExists(int id)
        {
            return (_context.EmployeePayHistories?.Any(e => e.BusinessEntityId == id)).GetValueOrDefault();
        }
    }
}
