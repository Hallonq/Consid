using Consid.Logic;
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
            libraryItemViewModel.LibraryItemList = _dbContext.LibraryItem.ToList();
            if (!string.IsNullOrEmpty(sortBy))
            {
                HttpContext.Session.SetString("sortBy", sortBy);
            }


            // default sorterat på categoryname
            //if (string.IsNullOrEmpty(_sortBy))
            //{
            //    libraryItemViewModel.LibraryItemList = _dbContext.LibraryItem.OrderBy(x => x.Category.CategoryName).ToList();
            //}

            // akronym på title
            libraryItemViewModel.LibraryItemList = LibraryItemLogic.AddAcronym(libraryItemViewModel.LibraryItemList);

            // sorter
            ViewBag.SortByCategoryName = string.IsNullOrEmpty(HttpContext.Session.GetString("sortBy")) ? "CategoryName" : "CategoryName";
            ViewBag.SortByType = HttpContext.Session.GetString("sortBy") == "Type" ? "Type" : "Type";

            libraryItemViewModel.LibraryItemList = LibraryItemLogic.Sorter(HttpContext.Session.GetString("sortBy"), libraryItemViewModel.LibraryItemList);

            return View(libraryItemViewModel);
        }

        public ActionResult Create()
        {
            ViewBag.Types = LibraryItemLogic.GetTypes();
            ViewBag.Categories = LibraryItemLogic.GetCategories(_dbContext);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LibraryItem libraryItem)
        {
            try
            {
                DatabaseLogic.CRUD(_dbContext, libraryItem, "Create");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            ViewBag.Types = LibraryItemLogic.GetTypes();
            ViewBag.Categories = LibraryItemLogic.GetCategories(_dbContext);

            LibraryItem libraryItem = _dbContext.LibraryItem.Where(x => x.Id == id).SingleOrDefault();

            return View(libraryItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LibraryItem libraryItem)
        {
            try
            {
                LibraryItemLogic.CheckInOrOut(_dbContext, libraryItem);
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
                DatabaseLogic.CRUD(_dbContext, _dbContext.LibraryItem.Where(x => x.Id == id).SingleOrDefault(), "Delete");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
