using System;
using System.Collections.Generic;

namespace WebMVC.Models
{
    public partial class BlogInfo
    {
        public int BlogId { get; set; }
        public string Title { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public DateTime DateOfCreation { get; set; }
        public string BlogUrl { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual EmpInfo EmailNavigation { get; set; } = null!;
    }
}
