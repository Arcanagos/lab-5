using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication6.Models;

namespace WebApplication6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBItemsController : ControllerBase
    {
        private readonly DBContext _context;

        public DBItemsController(DBContext context)
        {
            if(!context.DBItems.Any())
            {
                var pregenList = new List<DBItem>();

                var pos = new DBItem() { Id = 1, Name = "Adam", Surname = "Poziomka", PhoneNumber = 333222111, Email = "a.poziomka@net.com" };
                pregenList.Add(pos);

                var pos2 = new DBItem() { Id = 2, Name = "Dominika", Surname = "Jajko", PhoneNumber = 333222555, Email = "D.Jajko@net.com" };
                pregenList.Add(pos2);

                var pos3 = new DBItem() { Id = 3, Name = "Andrzej", Surname = "Popoj", PhoneNumber = 551222431, Email = "A.Popoj@net.com" };
                pregenList.Add(pos3);

                var pos4 = new DBItem() { Id = 4, Name = "Józef", Surname = "Jaba", PhoneNumber = 551222976, Email = "J.Jaba@wow.nrd" };
                pregenList.Add(pos4);

                context.DBItems.AddRange(pregenList);
                context.SaveChanges();
            }
            _context = context;
        }

        // GET
        [HttpGet("GET/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<DBItem>>> GetDBItems()
        {
            return Ok(await _context.DBItems.ToListAsync());
        }

        // PUT
        [HttpPut("PUT/")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> PutDBItem(long id, string name, string surname, long phonenumber, string email)
        {
            var dbItem = await _context.DBItems.FindAsync(id);
            if(name != null)
            {
                dbItem.Name = name;
            }
            if (surname != null)
            {
                dbItem.Surname = surname;
            }
            if (email != null)
            {
                dbItem.Email = email;
            }
            if (phonenumber != 0)
            {
                dbItem.PhoneNumber = phonenumber;
            }

            _context.Update(dbItem);
            try
            {
                await _context.SaveChangesAsync();

                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DBItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return NoContent();
                }
            }
        }

        // POST
        [HttpPost("POST/")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<DBItem>> PostDBItem(DBItem DBItem)
        {
            try
            {
                _context.DBItems.Add(DBItem);
                await _context.SaveChangesAsync();
            }
            catch
            {
            }
            return Created("POST/", DBItem);
        }

        // DELETE
        [HttpDelete("DELETE/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDBItem(long id)
        {
            var DBItem = await _context.DBItems.FindAsync(id);
            if (DBItem == null)
            {
                return NotFound();
            }

            _context.DBItems.Remove(DBItem);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound();
            }
        }

        private bool DBItemExists(long id)
        {
            return _context.DBItems.Any(e => e.Id == id);
        }
    }
}
