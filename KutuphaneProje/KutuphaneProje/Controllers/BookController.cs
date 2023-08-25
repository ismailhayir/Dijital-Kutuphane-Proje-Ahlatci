using KutuphaneProje.Data;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    private readonly MyDbContext _dbContext;

    public BookController(MyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var books = _dbContext.Kitaplar.ToList();
        return View(books);
    }
}
