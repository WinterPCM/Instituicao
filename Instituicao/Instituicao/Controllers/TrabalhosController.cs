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
        public async Task<IActionResult> Create(TrabalhoViewModel model)
        {
            if (ModelState.IsValid)
            {
                Trabalho novoTrabalho;
                switch (model.TipoTrabalho)
                {
                    case "TCC":
                        novoTrabalho = new TCC
                        {
                            TraID = model.TraID,
                            TraTitulo = model.TraTitulo,
                            TraValor = model.TraValor,
                            TraNota = model.TraNota,
                            DisID = model.DisID,
                            OrtID = model.OrtID
                        };
                        break;
                    case "Artigo":
                        novoTrabalho = new Artigo
                        {
                            TraID = model.TraID,
                            TraTitulo = model.TraTitulo,
                            TraValor = model.TraValor,
                            TraNota = model.TraNota,
                            DisID = model.DisID,
                            OrtID = model.OrtID
                        };
                        break;
                    case "Outro":
                        novoTrabalho = new Outro
                        {
                            TraID = model.TraID,
                            TraTitulo = model.TraTitulo,
                            TraValor = model.TraValor,
                            TraNota = model.TraNota,
                            DisID = model.DisID,
                            OrtID = model.OrtID
                        };
                        break;
                    default:
                        return View(model);
                }

                _context.Add(novoTrabalho);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", model.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", model.OrtID);
            return View(model);
        }


        // GET: Trabalhos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Buscar o trabalho no banco de dados
            var trabalho = await _context.Trabalhos.FindAsync(id);
            if (trabalho == null)
            {
                return NotFound();
            }

            // Converter a entidade `Trabalho` para `TrabalhoViewModel`
            var model = new TrabalhoViewModel
            {
                TraID = trabalho.TraID,
                TraTitulo = trabalho.TraTitulo,
                TraValor = trabalho.TraValor,
                TraNota = trabalho.TraNota,
                TipoTrabalho = trabalho.GetType().Name,
                DisID = trabalho.DisID ?? 0,  // Converter nullable para int
                OrtID = trabalho.OrtID ?? 0   // Converter nullable para int
            };

            // Preencher os dados para dropdowns
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", model.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", model.OrtID);

            // Retornar a View com o ViewModel
            return View(model);
        }

        // POST: Trabalhos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrabalhoViewModel model)
        {
            if (id != model.TraID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Buscar o trabalho original no banco de dados
                    var trabalho = await _context.Trabalhos.FindAsync(id);

                    if (trabalho == null)
                    {
                        return NotFound();
                    }

                    // Atualizar as propriedades do trabalho
                    trabalho.TraTitulo = model.TraTitulo;
                    trabalho.TraValor = model.TraValor;
                    trabalho.TraNota = model.TraNota;
                    trabalho.DisID = model.DisID;
                    trabalho.OrtID = model.OrtID;

                    // Salvar as alterações
                    _context.Update(trabalho);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                     if (!TrabalhoExists1(model.TraID))
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

            // Preencher os dados para dropdowns em caso de erro
            ViewData["DisID"] = new SelectList(_context.Disciplinas, "DisID", "DisID", model.DisID);
            ViewData["OrtID"] = new SelectList(_context.Orientadores, "OrtID", "OrtID", model.OrtID);

            return View(model);
        }

        private bool TrabalhoExists1(int id)
        {
            return _context.Trabalhos.Any(e => e.TraID == id);
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

        private bool TrabalhoExists2(int id)
        {
            return _context.Trabalhos.Any(e => e.TraID == id);
        }
    }
}
