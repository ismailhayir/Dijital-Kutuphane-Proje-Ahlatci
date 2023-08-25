namespace KutuphaneProje.Controllers
{
    using KutuphaneProje.Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Diagnostics;

    public class OkuController : Controller
    {
        private readonly MyDbContext _context; // Veritabanı bağlantısı

        public OkuController(MyDbContext context)
        {
            _context = context;
        }

        public ActionResult Details(int id)
        {
            var book = _context.Kitaplar.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
    }
}
