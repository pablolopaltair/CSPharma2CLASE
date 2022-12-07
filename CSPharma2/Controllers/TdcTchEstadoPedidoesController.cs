using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSPharma_DAL.Modelo;
using Microsoft.AspNetCore.Authorization;

namespace CSPharmaFinal.Controllers
{
    public class TdcTchEstadoPedidoesController : Controller
    {
        private readonly cspharma_informacionalContext _context;

        public TdcTchEstadoPedidoesController(cspharma_informacionalContext context)
        {
            _context = context;
        }

        // GET: TdcTchEstadoPedidoes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var cspharmaInformacionalContext = _context.TdcTchEstadoPedidos.Include(t => t.CodEstadoDevolucionNavigation).Include(t => t.CodEstadoEnvioNavigation).Include(t => t.CodEstadoPagoNavigation).Include(t => t.CodLineaNavigation);
            return View(await cspharmaInformacionalContext.ToListAsync());
        }

        // GET: TdcTchEstadoPedidoes/Details/5
        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Create
        [Authorize(Roles ="administrators")]
        public IActionResult Create()
        {
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion");
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio");
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago");
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea");
            return View();
        }

        // POST: TdcTchEstadoPedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Create([Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            bool valid = false;

            if (tdcTchEstadoPedido.CodEstadoDevolucionNavigation == null &&
                tdcTchEstadoPedido.CodEstadoEnvioNavigation == null &&
                tdcTchEstadoPedido.CodEstadoPagoNavigation == null &&
                tdcTchEstadoPedido.CodLineaNavigation == null)
            {
                valid = true;
            }
            if (valid)
            {
                _context.Add(tdcTchEstadoPedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Edit/5
        [Authorize(Roles = "administrators, employees")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // POST: TdcTchEstadoPedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators, employees")]
        public async Task<IActionResult> Edit(string id, [Bind("MdUuid,MdDate,Id,CodEstadoEnvio,CodEstadoPago,CodEstadoDevolucion,CodPedido,CodLinea")] TdcTchEstadoPedido tdcTchEstadoPedido)
        {
            long idDefault = _context.TdcTchEstadoPedidos.Where(x => x.MdUuid == tdcTchEstadoPedido.MdUuid).Select(x => x.Id).FirstOrDefault();

            tdcTchEstadoPedido.Id = default;

            if (idDefault != null)
            {
                try
                {
                    _context.Update(tdcTchEstadoPedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TdcTchEstadoPedidoExists(tdcTchEstadoPedido.MdUuid))
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
            ViewData["CodEstadoDevolucion"] = new SelectList(_context.TdcCatEstadosDevolucionPedidos, "CodEstadoDevolucion", "CodEstadoDevolucion", tdcTchEstadoPedido.CodEstadoDevolucion);
            ViewData["CodEstadoEnvio"] = new SelectList(_context.TdcCatEstadosEnvioPedidos, "CodEstadoEnvio", "CodEstadoEnvio", tdcTchEstadoPedido.CodEstadoEnvio);
            ViewData["CodEstadoPago"] = new SelectList(_context.TdcCatEstadosPagoPedidos, "CodEstadoPago", "CodEstadoPago", tdcTchEstadoPedido.CodEstadoPago);
            ViewData["CodLinea"] = new SelectList(_context.TdcCatLineasDistribucions, "CodLinea", "CodLinea", tdcTchEstadoPedido.CodLinea);
            return View(tdcTchEstadoPedido);
        }

        // GET: TdcTchEstadoPedidoes/Delete/5
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.TdcTchEstadoPedidos == null)
            {
                return NotFound();
            }

            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos
                .Include(t => t.CodEstadoDevolucionNavigation)
                .Include(t => t.CodEstadoEnvioNavigation)
                .Include(t => t.CodEstadoPagoNavigation)
                .Include(t => t.CodLineaNavigation)
                .FirstOrDefaultAsync(m => m.MdUuid == id);
            if (tdcTchEstadoPedido == null)
            {
                return NotFound();
            }

            return View(tdcTchEstadoPedido);
        }

        // POST: TdcTchEstadoPedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "administrators")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.TdcTchEstadoPedidos == null)
            {
                return Problem("Entity set 'CspharmaInformacionalContext.TdcTchEstadoPedidos'  is null.");
            }
            var tdcTchEstadoPedido = await _context.TdcTchEstadoPedidos.FindAsync(id);
            if (tdcTchEstadoPedido != null)
            {
                _context.TdcTchEstadoPedidos.Remove(tdcTchEstadoPedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TdcTchEstadoPedidoExists(string id)
        {
            return _context.TdcTchEstadoPedidos.Any(e => e.MdUuid == id);
        }
    }
}
