using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Consid
{
    public partial class Category
    {
        public Category()
        {
            LibraryItems = new HashSet<LibraryItem>();
        }

        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<LibraryItem> LibraryItems { get; set; }
    }
}
