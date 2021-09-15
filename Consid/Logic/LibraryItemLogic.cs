using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Logic
{
    public class LibraryItemLogic
    {
        // lägger till akronym i titel
        public static List<LibraryItem> AddAcronym(List<LibraryItem> libraryItemList)
        {
            foreach (var item in libraryItemList)
            {
                item.Title += $" ({ string.Join(string.Empty, item.Title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s[0].ToString().ToLower())) })";
            }

            return libraryItemList;
        }

        // sorterar libraryitem-table
        public static List<LibraryItem> Sorter(string sortBy, List<LibraryItem> libraryItemList)
        {
            switch (sortBy)
            {
                case "CategoryName":
                    return libraryItemList = libraryItemList.OrderBy(x => x.Category.CategoryName).ToList();
                case "Type":
                    return libraryItemList = libraryItemList.OrderBy(x => x.Type).ToList();
                default:
                    return libraryItemList = libraryItemList.OrderBy(x => x.Category.CategoryName).ToList();
            }
        }

        // hårdkodad typ-lista
        public static List<SelectListItem> GetTypes()
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
        public static List<SelectListItem> GetCategories(LibraryContext _dbContext)
        {
            List<Category> categories = _dbContext.Category.ToList();
            List<SelectListItem> selectCategoryItem = new List<SelectListItem>();
            foreach (var item in categories)
            {
                selectCategoryItem.Add(new SelectListItem() { Text = item.CategoryName, Value = item.Id.ToString() });
            }

            return selectCategoryItem;
        }

        // CHECK-(OUT/IN)
        public static void CheckInOrOut(LibraryContext _dbContext, LibraryItem libraryItem)
        {
            // check-out
            if (libraryItem.IsBorrowable && !string.IsNullOrEmpty(libraryItem.Borrower) && libraryItem.Type != "reference book")
            {
                libraryItem.BorrowDate = DateTime.Now;
                DatabaseLogic.CRUD(_dbContext, libraryItem, "Edit");
            }
            // check-in
            else if (!libraryItem.IsBorrowable)
            {
                libraryItem.BorrowDate = null;
                libraryItem.Borrower = null;
                DatabaseLogic.CRUD(_dbContext, libraryItem, "Edit");
            }
        }
    }
}
