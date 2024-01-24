using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DAL;

namespace WebApiMVC.Data
{
    public class WebApiMVCContext : DbContext
    {
        public WebApiMVCContext (DbContextOptions<WebApiMVCContext> options)
            : base(options)
        {
        }

        public DbSet<DAL.AdminInfo> AdminInfo { get; set; } = default!;

        public DbSet<DAL.BlogInfo>? BlogInfo { get; set; }

        public DbSet<DAL.EmpInfo>? EmpInfo { get; set; }
    }
}
