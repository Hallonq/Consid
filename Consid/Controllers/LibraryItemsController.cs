using Consid.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Consid.Controllers
{
    public class LibraryItemsController : Controller
    {
        private readonly ConsidContext _dbContext;

        public LibraryItemsController(ConsidContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index(/* sort by int index */)
        {
            LibraryItemViewModel libraryItemViewModel = new LibraryItemViewModel();
            libraryItemViewModel.LibraryItemList = _dbContext.LibraryItem.OrderBy(x => x.Category.CategoryName).ToList();
            libraryItemViewModel.CategoryList = _dbContext.Category.ToList();

            foreach (var item in libraryItemViewModel.LibraryItemList)
            {
                item.Title += $" ({ string.Join(string.Empty, item.Title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])) })";
            }

            return View(libraryItemViewModel);
        }

        public List<SelectListItem> GetTypes()
        {
            List<SelectListItem> types = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "book"},
                new SelectListItem() {Text = "reference book"},
                new SelectListItem() {Text = "dvd"},
                new SelectListItem() {Text = "audio book"}
            };

            return types;
        }

        public List<SelectListItem> GetCategories()
        {
            List<Category> categories = _dbContext.Category.ToList();
            List<SelectListItem> selectCategoryItem = new List<SelectListItem>();
            foreach (var item in categories)
            {
                selectCategoryItem.Add(new SelectListItem() { Text = item.CategoryName, Value = item.Id.ToString() });
            }

            return selectCategoryItem;
        }

        public ActionResult Create()
        {
            ViewBag.Types = GetTypes();
            ViewBag.Categories = GetCategories();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibraryItem libraryItem)
        {
            try
            {
                _dbContext.LibraryItem.Add(libraryItem);
                _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(LibraryItem libraryItem)
        {
            ViewBag.Types = GetTypes();

            return View(libraryItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LibraryItem libraryItem)
        {
            try
            {
                _dbContext.Entry(libraryItem).State = EntityState.Modified;
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
            return View();
        }
    }
}
