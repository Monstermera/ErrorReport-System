using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorSystem.Models;
using ErrorSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ErrorSystem.Contexts
{
    internal class DataContext : DbContext
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Hawaa\OneDrive\Skrivbord\ErrorSystem-experiment\ErrorSystem\Contexts\localdb.mdf;Integrated Security=True;Connect Timeout=30";

        #region constructors
        public DataContext()
        {
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        { 
        }
        #endregion

        #region overrides
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_connectionString);
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne(er => er.StatusReport)
                .WithMany()
                .HasForeignKey(er => er.StatusId);

            modelBuilder.Entity<CommentEntity>()
                .HasOne(er => er.Customer)
                .WithMany()
                .HasForeignKey(er => er.CustomId);

            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne(er => er.Customer)
                .WithMany()
                .HasForeignKey(er => er.CustomId);

            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne(e => e.Customer)
                .WithMany()
                .HasForeignKey(e => e.CustomId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne(e => e.Technician)
                .WithMany()
                .HasForeignKey(e => e.TechnicianId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ErrorReportEntity>()
                .HasOne(e => e.StatusReport)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ErrorReportEntity>()
                 .HasOne(e => e.Comment)
                 .WithMany()
                 .HasForeignKey(e => e.CommentId)
                 .OnDelete(DeleteBehavior.Cascade);
        }


        public DbSet<CustomerEntity> Customers { get; set; } = null!;
        public DbSet<AddressEntity> Addresses { get; set; } = null!;
        public DbSet<ErrorReportEntity> ErrorReports { get; set; } = null!;
        public DbSet<TechnicianEntity> Technicians { get; set; } = null!;
        public DbSet<StatusReportEntity> StatusReports { get; set; } = null!;
        public DbSet<CommentEntity> Comments { get; set; } = null!;

    }
}
