using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using url_shortener_mvc.Models;

namespace url_shortener_mvc.Controllers
{
    public class HomeController : Controller
    {
        private Context _context;

        public HomeController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ShortenUrl(string longUrl)
        {
            var url = new Url
            {
                Long = longUrl,
                Short = GenerateShortUrl(5)
            };

            _context.Urls.Add(url);
            _context.SaveChanges();

            return View("Index", url);
        }

        [HttpGet("r/{shortUrl}")]
        public IActionResult RedirectToLong(string shortUrl)
        {
            var url = _context.Urls.FirstOrDefault(u => u.Short == shortUrl);
            if (url == null)
                return NotFound();

            return Redirect(url.Long);
        }

        private string GenerateShortUrl(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();

            string result = "";
            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                result += chars[index];
            }

            return result;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
