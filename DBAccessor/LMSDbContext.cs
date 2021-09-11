using LibraryManagementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementSystem.DBAccessor
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<BookStore> BookStores { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }

    }
}
