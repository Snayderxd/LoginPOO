using Microsoft.AspNetCore.Mvc;
using ProyectoLogin.Models;
using System.Linq;

namespace ProyectoLogin.Controllers
{
    public class ProductosPanController : Controller
    {
        private readonly DbpruebaContext db;

        public ProductosPanController(DbpruebaContext context)
        {
            db = context;
        }

        // Acción para mostrar la lista de productos con búsqueda
        public IActionResult Index(string searchString)
        {
            var productos = from p in db.ProductosPan
                            select p;

            if (!string.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => p.Nombre.Contains(searchString));
            }

            return View(productos.ToList());
        }

        // Acción para mostrar los detalles de un producto
        public IActionResult Details(int id)
        {
            var producto = db.ProductosPan.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        // Acción para mostrar la vista de creación de producto
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductosPan producto)
        {
            if (ModelState.IsValid)
            {
                db.ProductosPan.Add(producto);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // Acción para mostrar la vista de edición de producto
        public IActionResult Edit(int id)
        {
            var producto = db.ProductosPan.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductosPan producto)
        {
            if (ModelState.IsValid)
            {
                db.ProductosPan.Update(producto);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // Acción para mostrar la vista de confirmación de eliminación
        public IActionResult Delete(int id)
        {
            var producto = db.ProductosPan.Find(id);
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var producto = db.ProductosPan.Find(id);
            if (producto != null)
            {
                db.ProductosPan.Remove(producto);
                db.SaveChanges();
                return RedirectToAction(nameof(DeleteSuccess));
            }
            return NotFound();
        }

        // Acción para mostrar la vista de confirmación de eliminación
        public IActionResult DeleteSuccess()
        {
            return View();
        }
    }
}
