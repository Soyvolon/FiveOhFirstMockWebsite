using FiveOhFirstMock.Data.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FiveOhFirstMock.Data
{
    public class ApplicationDbContext : IdentityDbContext<Trooper, TrooperRole, int>
    {
        public DbSet<QualificationSubmission> QualificationSubmissions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<QualificationSubmission>()
                .HasOne(p => p.Trooper)
                .WithMany(b => b.QualificationSubmissions)
                .HasForeignKey(p => p.TrooperId);

            builder.Entity<QualificationSubmission>()
                .HasMany(p => p.Instructors)
                .WithOne();

            builder.Entity<Trooper>()
                .HasMany(b => b.Flags)
                .WithOne()
                .IsRequired();
        }
    }
}
