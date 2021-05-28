using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consid.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ConsidContext _dbContext;

        public CategoryController(ConsidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            List<Category> categoryList = _dbContext.Category.ToList();

            return View(categoryList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {
                if (!_dbContext.Category.Where(x => x.CategoryName == category.CategoryName).Any())
                {
                    _dbContext.Category.Add(category);
                    _dbContext.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(string name)
        {
            ViewBag.Name = name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            try
            {
                _dbContext.Entry(category).State = EntityState.Modified;
                _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            // But if a category is referenced in any library item it cannot be deleted until the reference is removed first.
            try
            {
                _dbContext.Remove(_dbContext.Category.Where(x => x.Id == id).SingleOrDefault());
                _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
