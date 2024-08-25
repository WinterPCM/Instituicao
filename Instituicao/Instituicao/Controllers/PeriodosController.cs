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
    public class PeriodosController : Controller
    {
        private readonly InstituicaoDBContext _context;

        public PeriodosController(InstituicaoDBContext context)
        {
            _context = context;
        }

        // GET: Periodos
        public async Task<IActionResult> Index()
        {
            var instituicaoDBContext = _context.Periodos.Include(p => p.PerCurso);
            return View(await instituicaoDBContext.ToListAsync());
        }

        // GET: Periodos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodo = await _context.Periodos
                .Include(p => p.PerCurso)
                .FirstOrDefaultAsync(m => m.PerID == id);
            if (periodo == null)
            {
                return NotFound();
            }

            return View(periodo);
        }

        // GET: Periodos/Create
        public IActionResult Create()
        {
            ViewData["CurID"] = new SelectList(_context.Cursos, "CurID", "CurID");
            return View();
        }

        // POST: Periodos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PerID,PerNumero,PerSala,CurID")] Periodo periodo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(periodo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurID"] = new SelectList(_context.Cursos, "CurID", "CurID", periodo.CurID);
            return View(periodo);
        }

        // GET: Periodos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo == null)
            {
                return NotFound();
            }
            ViewData["CurID"] = new SelectList(_context.Cursos, "CurID", "CurID", periodo.CurID);
            return View(periodo);
        }

        // POST: Periodos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PerID,PerNumero,PerSala,CurID")] Periodo periodo)
        {
            if (id != periodo.PerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(periodo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeriodoExists(periodo.PerID))
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
            ViewData["CurID"] = new SelectList(_context.Cursos, "CurID", "CurID", periodo.CurID);
            return View(periodo);
        }

        // GET: Periodos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var periodo = await _context.Periodos
                .Include(p => p.PerCurso)
                .FirstOrDefaultAsync(m => m.PerID == id);
            if (periodo == null)
            {
                return NotFound();
            }

            return View(periodo);
        }

        // POST: Periodos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var periodo = await _context.Periodos.FindAsync(id);
            if (periodo != null)
            {
                _context.Periodos.Remove(periodo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeriodoExists(int id)
        {
            return _context.Periodos.Any(e => e.PerID == id);
        }
    }
}
