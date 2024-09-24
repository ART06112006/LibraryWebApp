using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Library_.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public string Photo { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public bool IsBorrowed { get; set; }
    }
}