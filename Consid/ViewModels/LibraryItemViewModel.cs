using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Consid.ViewModels
{
    public class LibraryItemViewModel
    {
        public LibraryItem LibraryItem { get; set; }
        public List<LibraryItem> LibraryItemList { get; set; }
        public List<Category> CategoryList { get; set; }

        public LibraryItemViewModel()
        {
            LibraryItemList = new List<LibraryItem>();
        }
    }
}
