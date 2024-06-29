using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocinaIdeal.Contexto;
using CocinaIdeal.Models;
using Microsoft.AspNetCore.Hosting;

namespace CocinaIdeal.Controllers
{
    public class CocinasController : Controller
    {
        private readonly MiContexto _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CocinasController(MiContexto context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Cocinas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cocinas.ToListAsync());
        }

        // GET: Cocinas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocina = await _context.Cocinas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocina == null)
            {
                return NotFound();
            }

            return View(cocina);
        }

        // GET: Cocinas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cocinas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CantidadStock,Marca,Modelo,ProductoFile,Precio")] Cocina cocina)
        {
            if (ModelState.IsValid)
            {
                if (cocina.ProductoFile != null)
                {
                    await GuardarImagen(cocina);
                }
                _context.Add(cocina);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cocina);
        }

        // GET: Cocinas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocina = await _context.Cocinas.FindAsync(id);
            if (cocina == null)
            {
                return NotFound();
            }
            return View(cocina);
        }

        // POST: Cocinas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CantidadStock,Marca,Modelo,ProductoFile,Precio")] Cocina cocina)
        {
            if (id != cocina.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (cocina.ProductoFile != null)
                    {
                        await GuardarImagen(cocina);
                    }

                    _context.Update(cocina);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CocinaExists(cocina.Id))
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
            return View(cocina);
        }

        private async Task GuardarImagen(Cocina cocina)
        {
            //throw new NotImplementedException();
            
            //formar el nombre de la foto
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string extension = Path.GetExtension(cocina.ProductoFile!.FileName);
            Guid id = Guid.NewGuid();
            string nameFoto = $"{cocina.Marca}_{id}{extension}";

            cocina.ImagenCocina = nameFoto;

            //copiar la foto en el proyecto
            string path = Path.Combine($"{wwwRootPath}/fotos/", nameFoto);
            var fileStream = new FileStream(path, FileMode.Create);
            await cocina.ProductoFile.CopyToAsync(fileStream);
            
        }
       

        // GET: Cocinas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cocina = await _context.Cocinas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cocina == null)
            {
                return NotFound();
            }

            return View(cocina);
        }

        // POST: Cocinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cocina = await _context.Cocinas.FindAsync(id);
            if (cocina != null)
            {
                _context.Cocinas.Remove(cocina);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CocinaExists(int id)
        {
            return _context.Cocinas.Any(e => e.Id == id);
        }
    }
}
