using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
   
    public class ToDoController : Controller
    {
        private readonly ToDoDbContext _context;

        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET: List all To Do Items ordered by creation date desc
        public async Task<IActionResult> Index()
        {
            var items = await _context.ToDoItems
                                      .OrderByDescending(x => x.CreatedDate)
                                      .ToListAsync();

            return View(items);
        }

        // GET: Display form to add new or edit existing To Do item
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new ToDoItem());
            }

            // Find existing item by id
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Save new or updated To Do item
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(ToDoItem model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Add new item
            if (model.Id == 0)
            {
                model.CreatedDate = DateTime.UtcNow;
                _context.ToDoItems.Add(model);
            }

            // Update existing item
            else
            {
                var existing = await _context.ToDoItems.FindAsync(model.Id);

                if (existing == null)
                {
                    return NotFound();
                }

                existing.Title = model.Title;
                existing.Description = model.Description;
                existing.IsDone = model.IsDone;
            }

            // Save changes to database
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // DELETE: Remove To Do item from database
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            // Remove item and save changes
            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
