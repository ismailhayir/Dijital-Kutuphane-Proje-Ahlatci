using System;
using System.Net.Http;
using System.Threading.Tasks;
using KutuphaneProje.Data;
using KutuphaneProje.Models;
using KutuphaneProje.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace KutuphaneProje.Controllers
{
    public class YorumlarController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly MyDbContext _context;

        public YorumlarController(IHttpClientFactory httpClientFactory, MyDbContext context)
        {
            _httpClientFactory = httpClientFactory;
            _context = context;
        }


        public async Task<IActionResult> Details()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var url = "https://localhost:7122/api/Comments"; // JSON verisi alacağınız URL'yi buraya girin

            try
            {
                var response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonContent = await response.Content.ReadAsStringAsync();
                    var detailsViewModel = JsonConvert.DeserializeObject<DetailsViewModel[]>(jsonContent);

                    return View(detailsViewModel); // Görünüme modeli iletiliyor.
                }
                else
                {
                    ViewBag.ErrorMessage = "HTTP request failed with status code: " + response.StatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                ViewBag.ErrorMessage = "HTTP request error: " + e.Message;
            }

            return View("Error");
        }

        public IActionResult YorumEkle()
        {
            return View();
        }

        [HttpPost]
        public IActionResult YorumEkle(Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Yorumlar.Add(yorum);
                _context.SaveChanges();
                return RedirectToAction("Details", "Yorumlar"); // veya istediğiniz sayfaya yönlendirme
            }
            return View(yorum);
        }
    }
}
