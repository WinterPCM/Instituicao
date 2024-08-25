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
    public class OrientadoresController : Controller
    {
        private readonly InstituicaoDBContext _context;

        public OrientadoresController(InstituicaoDBContext context)
        {
            _context = context;
        }

        // GET: Orientadores
        public async Task<IActionResult> Index()
        {
            var instituicaoDBContext = _context.Orientadores.Include(o => o.OrtProfessor);
            return View(await instituicaoDBContext.ToListAsync());
        }

        // GET: Orientadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores
                .Include(o => o.OrtProfessor)
                .FirstOrDefaultAsync(m => m.OrtID == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // GET: Orientadores/Create
        public IActionResult Create()
        {
            ViewData["ProMatricula"] = new SelectList(_context.Set<Professor>(), "UsuMatricula", "TipoUsuario");
            return View();
        }

        // POST: Orientadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrtID,ProMatricula")] Orientador orientador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orientador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProMatricula"] = new SelectList(_context.Set<Professor>(), "UsuMatricula", "TipoUsuario", orientador.ProMatricula);
            return View(orientador);
        }

        // GET: Orientadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores.FindAsync(id);
            if (orientador == null)
            {
                return NotFound();
            }
            ViewData["ProMatricula"] = new SelectList(_context.Set<Professor>(), "UsuMatricula", "TipoUsuario", orientador.ProMatricula);
            return View(orientador);
        }

        // POST: Orientadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrtID,ProMatricula")] Orientador orientador)
        {
            if (id != orientador.OrtID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orientador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrientadorExists(orientador.OrtID))
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
            ViewData["ProMatricula"] = new SelectList(_context.Set<Professor>(), "UsuMatricula", "TipoUsuario", orientador.ProMatricula);
            return View(orientador);
        }

        // GET: Orientadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orientador = await _context.Orientadores
                .Include(o => o.OrtProfessor)
                .FirstOrDefaultAsync(m => m.OrtID == id);
            if (orientador == null)
            {
                return NotFound();
            }

            return View(orientador);
        }

        // POST: Orientadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orientador = await _context.Orientadores.FindAsync(id);
            if (orientador != null)
            {
                _context.Orientadores.Remove(orientador);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrientadorExists(int id)
        {
            return _context.Orientadores.Any(e => e.OrtID == id);
        }
    }
}
