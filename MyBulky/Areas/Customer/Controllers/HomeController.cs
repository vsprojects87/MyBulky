using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBulky.Data;
using MyBulky.Models;
using System.Diagnostics;

namespace MyBulky.Areas.Customer.Controllers
{
	[Area("Customer")]

	public class HomeController : Controller
    {
		private readonly AppDBContext _context;

        public HomeController(AppDBContext context)
        {
			_context = context;
		}

		public async Task<IActionResult> IndexAsync()
        {
			var appDBContext = _context.Products.Include(p => p.Category);
			return View(await appDBContext.ToListAsync());
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = await _context.Products
				.Include(p => p.Category)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
