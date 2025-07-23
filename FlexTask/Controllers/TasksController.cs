using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlexTask.Data;
using FlexTask.Models;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace FlexTask.Controllers
{
    public class TasksController : Controller
    {
        private readonly AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _context.TodoTasks.OrderByDescending(t => t.CreatedAt).ToListAsync();
            return View(tasks);
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoTask task)
        {
            if (ModelState.IsValid)
            {
                task.CreatedAt = DateTime.Now;
                task.IsCompleted = false;
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,IsCompleted,CreatedAt")] TodoTask task)
        {
            if (id != task.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _context.Update(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task != null)
            {
                _context.TodoTasks.Remove(task);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/ToggleComplete/5
        [HttpPost]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var task = await _context.TodoTasks.FindAsync(id);
            if (task == null) return NotFound();
            task.IsCompleted = !task.IsCompleted;
            _context.Update(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
} 