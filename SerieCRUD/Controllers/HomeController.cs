using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SerieCRUD.Models;

namespace SerieCRUD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Serie Serie { get; set; }
        public IEnumerable<Serie> Series { get; set; }

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ViewResult> Index()
        {
            Series = await _db.Serie.ToListAsync();
            Series = Series.Where(serie => serie.Excluido == false);
            return View(Series);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var serie = await _db.Serie.FindAsync(id);
            if (serie == null)
            {
                return NotFound();
            }
            serie.Excluido = true;
            _db.Serie.Update(serie);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Upsert(int? id)
        {
            Serie = new Serie();
            if (id == null)
            {
                //create
                return PartialView("Upsert", Serie);
            }
            //update
            Serie = _db.Serie.FirstOrDefault(u => u.Id == id);
            if (Serie == null)
            {
                return NotFound();
            }
            return PartialView("Upsert", Serie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert( )
        {
            if (ModelState.IsValid)
            {
                if (Serie.Id == 0)
                {
                    //create
                    _db.Serie.Add(Serie);
                }
                else 
                {
                    _db.Serie.Update(Serie);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Serie);
        }

    } 
}
