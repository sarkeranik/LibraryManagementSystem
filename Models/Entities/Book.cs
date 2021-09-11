using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Entities
{
    public class Book
    {
        public Book()
        {
            Author = new HashSet<Author>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PublisherName { get; set; }
        public int TotalSold { get; set; }
        public DateTime SoldDate { get; set; }

        public virtual ICollection<Author> Author { get; set; }

        public virtual BookStore StoreId { get; set; }
    }
}
