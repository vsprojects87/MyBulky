using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyBulky.Data;
using MyBulky.Models;

namespace MyBulky.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoriesController : Controller
    {

		private readonly IUnitOfWork _unitOfWork;

		public CategoriesController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

		// GET: Categories
		public IActionResult Index()
        {
			//List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
			//return View(objCategoryList);
            // best practice

            return View(_unitOfWork.Category.GetAll().ToList());
        }


        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryId,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
				_unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.Category.Get(x=>x.CategoryId==id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,Name")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.Category.Update(category);
                    _unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _unitOfWork.Category
                .Get(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _unitOfWork.Category.Get(x=>x.CategoryId==id);
            if (category != null)
            {
                _unitOfWork.Category.Delete(category);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
			Category cat = _unitOfWork.Category.Get(e => e.CategoryId == id);
            if (cat == null) {
                return false;
			}
            return true;
        }
    }
}
