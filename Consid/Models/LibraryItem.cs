using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library
{
    public partial class LibraryItem
    {
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? Pages { get; set; }
        public int? RunTimeMinutes { get; set; }
        public bool IsBorrowable { get; set; }
        public string Borrower { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? BorrowDate { get; set; }
        public string Type { get; set; }

        public virtual Category Category { get; set; }
    }
}
