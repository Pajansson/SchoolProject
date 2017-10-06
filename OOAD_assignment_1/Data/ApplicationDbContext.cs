using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OOAD_assignment_1.Models;

namespace OOAD_assignment_1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Accountability>().HasKey(a => new { a.AccountableId, a.CommissionerId, a.AccountabilityTypeId });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public virtual DbSet<Party> Parties { get; set; }
        public virtual DbSet<Accountability> Accountabilities { get; set; }
        public virtual DbSet<AccountabilityType> AccountabilityTypes { get; set; }
    }
}
