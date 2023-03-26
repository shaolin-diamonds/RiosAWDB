﻿using System;
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
    public class EmployeeDepartmentHistoriesController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public EmployeeDepartmentHistoriesController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        // GET: api/EmployeeDepartmentHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDepartmentHistory>>> GetEmployeeDepartmentHistories()
        {
          if (_context.EmployeeDepartmentHistories == null)
          {
              return NotFound();
          }
            return await _context.EmployeeDepartmentHistories.ToListAsync();
        }

        // GET: api/EmployeeDepartmentHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDepartmentHistory>> GetEmployeeDepartmentHistory(int id)
        {
          if (_context.EmployeeDepartmentHistories == null)
          {
              return NotFound();
          }
            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistories.FindAsync(id);

            if (employeeDepartmentHistory == null)
            {
                return NotFound();
            }

            return employeeDepartmentHistory;
        }

        // PUT: api/EmployeeDepartmentHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployeeDepartmentHistory(int id, EmployeeDepartmentHistory employeeDepartmentHistory)
        {
            if (id != employeeDepartmentHistory.BusinessEntityId)
            {
                return BadRequest();
            }

            _context.Entry(employeeDepartmentHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeDepartmentHistoryExists(id))
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

        // POST: api/EmployeeDepartmentHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeDepartmentHistory>> PostEmployeeDepartmentHistory(EmployeeDepartmentHistory employeeDepartmentHistory)
        {
          if (_context.EmployeeDepartmentHistories == null)
          {
              return Problem("Entity set 'AdventureWorks2019Context.EmployeeDepartmentHistories'  is null.");
          }
            _context.EmployeeDepartmentHistories.Add(employeeDepartmentHistory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (EmployeeDepartmentHistoryExists(employeeDepartmentHistory.BusinessEntityId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetEmployeeDepartmentHistory", new { id = employeeDepartmentHistory.BusinessEntityId }, employeeDepartmentHistory);
        }

        // DELETE: api/EmployeeDepartmentHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeDepartmentHistory(int id)
        {
            if (_context.EmployeeDepartmentHistories == null)
            {
                return NotFound();
            }
            var employeeDepartmentHistory = await _context.EmployeeDepartmentHistories.FindAsync(id);
            if (employeeDepartmentHistory == null)
            {
                return NotFound();
            }

            _context.EmployeeDepartmentHistories.Remove(employeeDepartmentHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeDepartmentHistoryExists(int id)
        {
            return (_context.EmployeeDepartmentHistories?.Any(e => e.BusinessEntityId == id)).GetValueOrDefault();
        }
    }
}
