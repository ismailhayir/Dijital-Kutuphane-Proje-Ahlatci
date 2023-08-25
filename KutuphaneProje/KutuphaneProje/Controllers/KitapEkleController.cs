using KutuphaneProje.Data;
using KutuphaneProje.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace KutuphaneProje.Controllers
{
    public class KitapEkleController : Controller
    {
        private readonly MyDbContext _context;

        public KitapEkleController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult KitapEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult KitapEkle(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Kitaplar.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Kitaplar", "Admin"); // veya istediğiniz sayfaya yönlendirme
            }
            return View(book);
        }
    }

}
