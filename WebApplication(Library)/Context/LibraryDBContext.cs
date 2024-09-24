using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApplication_Library_.Models;

namespace WebApplication_Library_.Context
{
    public class LibraryDBContext : DbContext
    {
        public LibraryDBContext() : base("LibraryDbConnectionString")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}