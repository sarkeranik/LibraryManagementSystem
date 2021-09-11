using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Entities
{
    public class Sale
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public int TotalBookSale { get; set; }
        public int BookId { get; set; }

        public int StoreId { get; set; }

    }
}
