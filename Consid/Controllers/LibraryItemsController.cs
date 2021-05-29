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

        public ActionResult Index(LibraryItemViewModel libraryItemViewModel, string sortBy)
        {
            libraryItemViewModel.CategoryList = _dbContext.Category.ToList();

            // default sorterat på categoryname
            libraryItemViewModel.LibraryItemList = _dbContext.LibraryItem.OrderBy(x => x.Category.CategoryName).ToList();

            // akronym på title
            foreach (var item in libraryItemViewModel.LibraryItemList)
            {
                item.Title += $" ({ string.Join(string.Empty, item.Title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0])) })";
            }

            // sorter
            ViewBag.SortByCategoryName = string.IsNullOrEmpty(sortBy) ? "CategoryName" : "CategoryName";
            ViewBag.SortByType = sortBy == "Type" ? "Type" : "Type";

            switch (sortBy)
            {
                case "CategoryName":
                    libraryItemViewModel.LibraryItemList = libraryItemViewModel.LibraryItemList.OrderBy(x => x.Category.CategoryName).ToList();
                    break;
                case "Type":
                    libraryItemViewModel.LibraryItemList = libraryItemViewModel.LibraryItemList.OrderBy(x => x.Type).ToList();
                    break;
                default:
                    libraryItemViewModel.LibraryItemList = libraryItemViewModel.LibraryItemList.OrderBy(x => x.Category.CategoryName).ToList();
                    break;
            }

            return View(libraryItemViewModel);
        }

        // hårdkodad typ-lista
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

        // returnerar kategorilista som SelectListItem object
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

        public ActionResult Edit(int id)
        {
            ViewBag.Types = GetTypes();
            ViewBag.Categories = GetCategories();

            LibraryItem libraryItem = _dbContext.LibraryItem.Where(x => x.Id == id).SingleOrDefault();

            return View(libraryItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LibraryItem libraryItem)
        {
            try
            {
                libraryItem.BorrowDate = DateTime.Now;
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
            try
            {
                _dbContext.Remove(_dbContext.LibraryItem.Where(x => x.Id == id).SingleOrDefault());
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
