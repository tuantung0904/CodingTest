using CodingTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ToDoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoModel>>> GetToDoItems()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        // GET: api/ToDo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoModel>> GetToDoModel(int id)
        {
            var toDoModel = await _context.ToDoItems.FindAsync(id);

            if (toDoModel == null)
            {
                return NotFound();
            }

            return toDoModel;
        }

        // PUT: api/ToDo/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoModel(int id, ToDoModel toDoModel)
        {
            if (id != toDoModel.Id)
            {
                return BadRequest();
            }

            if (toDoModel.DueDate <= DateTime.Now)
            {
                return NotFound();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoModelExists(id))
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

        // POST: api/ToDo
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ToDoModel>> PostToDoModel(ToDoModel toDoModel)
        {
            if (toDoModel == null || toDoModel.DueDate <= DateTime.Now)
            {
                return NotFound();
            }

            _context.ToDoItems.Add(toDoModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoModel", new { id = toDoModel.Id }, toDoModel);
        }

        // DELETE: api/ToDo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ToDoModel>> DeleteToDoModel(int id)
        {
            var toDoModel = await _context.ToDoItems.FindAsync(id);
            if (toDoModel == null)
            {
                return NotFound();
            }

            _context.ToDoItems.Remove(toDoModel);
            await _context.SaveChangesAsync();

            return toDoModel;
        }

        private bool ToDoModelExists(int id)
        {
            return _context.ToDoItems.Any(e => e.Id == id);
        }
    }
}
