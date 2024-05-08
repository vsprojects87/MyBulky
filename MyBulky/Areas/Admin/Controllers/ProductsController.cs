using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBulky.Data;
using MyBulky.Models;

namespace MyBulky.Areas.Admin.Controllers
{
	[Area("Admin")]

	public class ProductsController : Controller
	{
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IUnitOfWork _unitOfWork;
		private readonly AppDBContext _context;

		public ProductsController(IUnitOfWork unitOfWork, AppDBContext context, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_context = context;

		}

		// GET: Products
		public IActionResult Index()
		{
			var appDBContext = _context.Products.Include(p => p.Category);
			return View(appDBContext.ToList());
		}


		// GET: Products/Create
		public IActionResult Create()
		{

			ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "Name");
			return View();
		}

		// POST: Products/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(
			[Bind("Id,Title,Description,ISBN,Author,Price,CategoryId,ImgUrl")] Product product,
			IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"images\product");
					if (!string.IsNullOrEmpty(product.ImgUrl))
					{
						var oldImagePath = Path.Combine(wwwRootPath, product.ImgUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagePath))
						{
							System.IO.File.Delete(oldImagePath);
						}
					}
					using (var fileStream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}
					product.ImgUrl = @"\images\product\" + filename;
				}
				ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "Name", product.CategoryId);
				_unitOfWork.Product.Add(product);
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));
			}
			else
			{
				return View(product);
			}
		}


		// GET: Products/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = _unitOfWork.Product.Get(x => x.Id == id);
			if (product == null)
			{
				return NotFound();
			}
			ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		// POST: Products/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ISBN,Author,Price,CategoryId,ImgUrl")] Product product)
		{
			if (id != product.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				_unitOfWork.Product.Update(product);
				_unitOfWork.Save();
				return RedirectToAction(nameof(Index));
			}
			ViewData["CategoryId"] = new SelectList(_unitOfWork.Category.GetAll(), "CategoryId", "Name", product.CategoryId);
			return View(product);
		}

		// GET: Products/Delete/5
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var product = _unitOfWork.Product.Get(m => m.Id == id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
		}

		// POST: Products/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			var product = _unitOfWork.Product.Get(x => x.Id == id);
			if (product != null)
			{
				_unitOfWork.Product.Delete(product);
			}

			_unitOfWork.Save();
			return RedirectToAction(nameof(Index));
		}


	}
}
