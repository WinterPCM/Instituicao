using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Instituicao.Data;
using Instituicao.Models;

namespace Instituicao.Controllers
{
    public class TrabalhosController : Controller
    {
        private readonly InstituicaoDBContext _context;

        public TrabalhosController(InstituicaoDBContext context)
        {
            _context = context;
        }

        // GET: Trabalhos
        public async Task<IActionResult> Index()
        {
            var instituicaoDBContext = _context.Trabalhos.Include(t => t.TraDisciplina).Include(t => t.TraOrientador);
            return View(await instituicaoDBContext.ToListAsync());
        }

        // GET: Trabalhos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalho = await _context.Trabalhos
                .Include(t => t.TraDisciplina)
                .Include(t => t.TraOrientador)
                .FirstOrDefaultAsync(m => m.TraID == id);
            if (trabalho == null)
            {
                return NotFound();
            }

            return View(trabalho);
        }

        // GET: Trabalhos/Create
        public IActionResult Create()
        {
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID");
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID");
            return View();
        }

        // POST: Trabalhos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TraID,TraTitulo,TraValor,TraNota,DisID,OrtID")] Trabalho trabalho)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", trabalho.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", trabalho.OrtID);
            return View(trabalho);
        }

        // GET: Trabalhos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalho = await _context.Trabalhos.FindAsync(id);
            if (trabalho == null)
            {
                return NotFound();
            }
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", trabalho.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", trabalho.OrtID);
            return View(trabalho);
        }

        // POST: Trabalhos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TraID,TraTitulo,TraValor,TraNota,DisID,OrtID")] Trabalho trabalho)
        {
            if (id != trabalho.TraID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabalho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabalhoExists(trabalho.TraID))
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
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", trabalho.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", trabalho.OrtID);
            return View(trabalho);
        }

        // GET: Trabalhos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabalho = await _context.Trabalhos
                .Include(t => t.TraDisciplina)
                .Include(t => t.TraOrientador)
                .FirstOrDefaultAsync(m => m.TraID == id);
            if (trabalho == null)
            {
                return NotFound();
            }

            return View(trabalho);
        }

        // POST: Trabalhos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabalho = await _context.Trabalhos.FindAsync(id);
            if (trabalho != null)
            {
                _context.Trabalhos.Remove(trabalho);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabalhoExists(int id)
        {
            return _context.Trabalhos.Any(e => e.TraID == id);
        }
    }
}
