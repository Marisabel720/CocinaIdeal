using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CocinaIdeal.Contexto;
using CocinaIdeal.Models;

namespace CocinaIdeal.Controllers
{
    public class VentasController : Controller
    {
        private readonly MiContexto _context;

        public VentasController(MiContexto context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var miContexto = _context.Ventas.Include(v => v.Cliente).Include(v => v.Cocina).Include(v => v.Usuario);
            return View(await miContexto.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Cocina)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Info");
            ViewData["CocinaId"] = new SelectList(_context.Cocinas, "Id", "Id");
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumeroRecibo,FechaCompra,EsPagado,NumeroTicketReserva,Precio,UsuarioId,CocinaId,ClienteId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                venta.FechaCompra = DateTime.Now;
                venta.NumeroRecibo = GetNumero();

                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Ci", venta.ClienteId);
            ViewData["CocinaId"] = new SelectList(_context.Cocinas, "Id", "Id", venta.CocinaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", venta.UsuarioId);
            return View(venta);
        }

        private int GetNumero()
        {
            if (_context.Ventas.ToList().Count > 0)
                return _context.Ventas.Max(i => i.NumeroRecibo) + 1;
            return 1;
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Ci", venta.ClienteId);
            ViewData["CocinaId"] = new SelectList(_context.Cocinas, "Id", "Id", venta.CocinaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", venta.UsuarioId);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumeroRecibo,FechaCompra,EsPagado,NumeroTicketReserva,Precio,UsuarioId,CocinaId,ClienteId")] Venta venta)
        {
            if (id != venta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "Ci", venta.ClienteId);
            ViewData["CocinaId"] = new SelectList(_context.Cocinas, "Id", "Id", venta.CocinaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Email", venta.UsuarioId);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Cocina)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.Id == id);
        }
    }
}
