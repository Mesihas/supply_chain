using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EFDC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EFDC.Services
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Order> TblOrder { get; set; }
        public DbSet<OrderDetail> TblOrderDetail { get; set; }
        public DbSet<Product> TblProduct { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<SubCategory> TblSubCategory { get; set; }
        public DbSet<Branch> TblBranch { get; set; }
        public DbSet<OrderState> TblOrderState { get; set; }
        public DbSet<Profile> TblProfile { get; set; }
        public DbSet<ProductAvalilability> TblProductAvalilability { get; set;}
        public DbSet<Mail> TblMail { get; set; }
        public DbSet<Puesto> TblPuesto { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            //   option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database= EFDCVolta ; Trusted_Connection=True;");
            option.UseSqlServer(@"Server=DOTNETDEV ; Database= VoltaNew ; Trusted_Connection=True;");
        }
    }
}
