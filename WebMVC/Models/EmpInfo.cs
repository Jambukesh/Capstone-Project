using System;
using System.Collections.Generic;

namespace WebMVC.Models
{
    public partial class EmpInfo
    {
        public EmpInfo()
        {
            BlogInfos = new HashSet<BlogInfo>();
        }

        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public DateTime DateOfJoining { get; set; }
        public int PassCode { get; set; }

        public virtual ICollection<BlogInfo> BlogInfos { get; set; }
    }
}
