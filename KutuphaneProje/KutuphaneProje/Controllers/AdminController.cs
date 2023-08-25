using KutuphaneProje.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using KutuphaneProje.Models;

public class AdminController : Controller
{
    private readonly MyDbContext _context;

    public AdminController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username == username && a.Password == password);
        if (admin != null)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, admin.Username),
            // Diğer claim'leri ekleme
        };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30), // Oturum süresi
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Dashboard","Admin");
        }

        ModelState.AddModelError("", "Bilgiler hatalı");
        return View();
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
            ViewBag.Message = "Kitap Başarıyla Eklendi!";
            return RedirectToAction("Index", "Book"); // veya istediğiniz sayfaya yönlendirme
        }
        return View(book);
    }

    public IActionResult Kitaplar()
    {
        var data = _context.Kitaplar.ToList();



        return View(data);

    }
    public IActionResult Yorumlar()
    {
        var yorumdata  = _context.Yorumlar.ToList();



        return View(yorumdata);

    }

    public IActionResult List()
    {
        var data = _context.Kitaplar.ToList(); // "Veriler" tablosundaki verileri alın
        return View(data);
    }

    public IActionResult Delete(int id)
    {
        var dataToDelete = _context.Kitaplar.Find(id); // Veriyi id'ye göre bul
        if (dataToDelete != null)
        {
            _context.Kitaplar.Remove(dataToDelete); // Veriyi sil
            _context.SaveChanges(); // Değişiklikleri kaydet
        }
        return RedirectToAction("Kitaplar");
    }

    public IActionResult YorumSil(int id)
    {
        var dataToDelete = _context.Yorumlar.Find(id); // Veriyi id'ye göre bul
        if (dataToDelete != null)
        {
            _context.Yorumlar.Remove(dataToDelete); // Veriyi sil
            _context.SaveChanges(); // Değişiklikleri kaydet
        }
        return RedirectToAction("Yorumlar");
    }

    public IActionResult KitapGuncelle(int id)
    {
        var dataToEdit = _context.Kitaplar.Find(id); // Veriyi id'ye göre bul
        if (dataToEdit != null)
        {
            return View(dataToEdit); // Veriyi güncelleme view'ine yönlendir
        }
        return RedirectToAction("Kitaplar");
    }

    [HttpPost]
    public IActionResult Update(Book updatedData)
    {
        if (ModelState.IsValid)
        {
            _context.Kitaplar.Update(updatedData); // Veriyi güncelle
            _context.SaveChanges(); // Değişiklikleri kaydet
            TempData["SuccessMessage"] = "Kitap başarıyla güncellendi.";
            return RedirectToAction("Kitaplar");
        }
        return View(updatedData);
    }

}
