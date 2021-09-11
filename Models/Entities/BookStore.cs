using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Entities
{
    public class BookStore
    {
        public BookStore()
        {
            Book = new HashSet<Book>();
            Sale = new HashSet<Sale>();
        }

        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Book { get; set; }
        public virtual ICollection<Sale> Sale{ get; set; }
    }
}
