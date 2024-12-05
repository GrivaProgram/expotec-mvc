using expotec_mvc.Data;
using expotec_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace expotec_mvc.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> MarkAttendance(int id)
        {
            // Buscar el contacto por su Id
            var contact = await _context.Contact.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            // Marcar asistencia
            contact.AttendedExpotec = true; // Cambiar a true
            _context.Update(contact);
            await _context.SaveChangesAsync();

            // Redirigir de nuevo a la lista de contactos
            return RedirectToAction(nameof(Index));
        }

        // Acción para listar todos los contactos
        public IActionResult Index(string searchTerm)
        {
            // Si searchTerm es null o vacío, mostramos todos los registros
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "" : searchTerm;

            var model = _context.Contact
                                .Where(c => c.Name.Contains(searchTerm) || c.Dni.Contains(searchTerm)) // Filtrar por campos relevantes
                                .ToList();

            // Mantener el término de búsqueda en la vista
            ViewData["SearchTerm"] = searchTerm;

            return View(model); // Regresar la vista con los resultados filtrados
        }


        // Acción para mostrar detalles de un contacto
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Acción para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Acción para procesar la creación de un contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Dni,Phone,Ruc,Position")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contact);
        }

        // Acción para mostrar el formulario de edición
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Acción para procesar la edición de un contacto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Dni,Phone,Email,Category,Position,CompanyName,Area,CocktailCandidate,Source,Ruc,Address,District,AttendedExpotec,AttendedCocktail,Coordinator,Seller,Comments")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
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
            return View(contact);
        }

        // Acción para mostrar el formulario de eliminación
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FirstOrDefaultAsync(c => c.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // Acción para procesar la eliminación de un contacto
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact != null)
            {
                _context.Contact.Remove(contact);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
