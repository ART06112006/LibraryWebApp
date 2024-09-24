using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication_Library_.Models
{
    public enum Role
    {
        User,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> BorrowedBooks { get; set; }
    }
}