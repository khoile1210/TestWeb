using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class NotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NotesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Notes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Note.ToListAsync());
        }

        // GET: SearchNotes
        public async Task<IActionResult> SearchNotes()
        {
            return View();
        }

        // POST: SearchNotesResult
        public async Task<IActionResult> SearchNotesResult(string NoteTitle)
        {
            return View("Index",await _context.Note.Where(j=>j.NoteTitle.Contains(NoteTitle)).ToListAsync());
        }

        // GET: Notes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Notes/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NoteId,Author,NoteTitle,NoteMessage")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Notes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NoteId,Author,NoteTitle,NoteMessage")] Note note)
        {
            if (id != note.NoteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.NoteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        // GET: Notes/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.NoteId == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Notes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var note = await _context.Note.FindAsync(id);
            if (note != null)
            {
                _context.Note.Remove(note);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
            return _context.Note.Any(e => e.NoteId == id);
        }
    }
}
