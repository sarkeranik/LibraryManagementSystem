using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Entities
{
    public class Author
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MaxLength(100)]
        public string  Name { get; set; }
        public string Address { get; set; }

        public virtual Book BookId { get; set; }

    }
}
