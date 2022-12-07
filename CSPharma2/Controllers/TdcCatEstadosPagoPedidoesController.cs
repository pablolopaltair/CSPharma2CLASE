using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSPharma_DAL.Modelo;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CSPharma2.Controllers
{
    public class TdcCatEstadosPagoPedidoesController : Controller
    {
        private readonly cspharma_informacionalContext _context;

        public TdcCatEstadosPagoPedidoesController(cspharma_informacionalContext context)
        {
            _context = context;
        }

        // GET: TdcCatEstadosPagoPedidoes
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Index()
        {
              return View(await _context.TdcCatEstadosPagoPedidos.ToListAsync());
        }

        // GET: TdcCatEstadosPagoPedidoes/Details/5
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TdcCatEstadosPagoPedidos == null)
            {
                return NotFound();
            }

            var tdcCatEstadosPagoPedido = await _context.TdcCatEstadosPagoPedidos
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcCatEstadosPagoPedido == null)
            {
                return NotFound();
            }

            return View(tdcCatEstadosPagoPedido);
        }

        // GET: TdcCatEstadosPagoPedidoes/Create
        [Authorize(Roles = "administrators")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TdcCatEstadosPagoPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Create([Bind("MdUuid,MdDate,Id,CodEstadoPago,DesEstadoPago")] TdcCatEstadosPagoPedido tdcCatEstadosPagoPedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tdcCatEstadosPagoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tdcCatEstadosPagoPedido);
        }

        // GET: TdcCatEstadosPagoPedidoes/Edit/5
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TdcCatEstadosPagoPedidos == null)
            {
                return NotFound();
            }

            var tdcCatEstadosPagoPedido = await _context.TdcCatEstadosPagoPedidos.FindAsync(id);
            if (tdcCatEstadosPagoPedido == null)
            {
                return NotFound();
            }
            return View(tdcCatEstadosPagoPedido);
        }

        // POST: TdcCatEstadosPagoPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Edit(string id, [Bind("MdUuid,MdDate,Id,CodEstadoPago,DesEstadoPago")] TdcCatEstadosPagoPedido tdcCatEstadosPagoPedido)
        {
            if (id != tdcCatEstadosPagoPedido.MdUuid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tdcCatEstadosPagoPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcCatEstadosPagoPedidoExists(tdcCatEstadosPagoPedido.MdUuid))
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
            return View(tdcCatEstadosPagoPedido);
        }

        // GET: TdcCatEstadosPagoPedidoes/Delete/5
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TdcCatEstadosPagoPedidos == null)
            {
                return NotFound();
            }

            var tdcCatEstadosPagoPedido = await _context.TdcCatEstadosPagoPedidos
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcCatEstadosPagoPedido == null)
            {
                return NotFound();
            }

            return View(tdcCatEstadosPagoPedido);
        }

        // POST: TdcCatEstadosPagoPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TdcCatEstadosPagoPedidos == null)
            {
                return Problem("Entity set 'cspharma_informacionalContext.TdcCatEstadosPagoPedidos'  is null.");
            }
            var tdcCatEstadosPagoPedido = await _context.TdcCatEstadosPagoPedidos.FindAsync(id);
            if (tdcCatEstadosPagoPedido != null)
            {
                _context.TdcCatEstadosPagoPedidos.Remove(tdcCatEstadosPagoPedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdcCatEstadosPagoPedidoExists(string id)
        {
          return _context.TdcCatEstadosPagoPedidos.Any(e => e.MdUuid == id);
        }
    }
}
