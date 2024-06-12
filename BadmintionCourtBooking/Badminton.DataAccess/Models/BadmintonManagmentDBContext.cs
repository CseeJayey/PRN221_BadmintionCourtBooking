using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Badminton.DataAccess.Models
{
    public partial class BadmintonManagmentDBContext : DbContext
    {
        public BadmintonManagmentDBContext()
        {
        }

        public BadmintonManagmentDBContext(DbContextOptions<BadmintonManagmentDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<Court> Courts { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<ManagerCourt> ManagerCourts { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<StaffCourt> StaffCourts { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=12345;database=BadmintonManagment;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.BookingId)
                    .HasMaxLength(50)
                    .HasColumnName("BookingID");

                entity.Property(e => e.BookingType).HasMaxLength(50);

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

                entity.Property(e => e.TotalHours).HasMaxLength(10);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Bookings_Customers");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.TimeSlotId)
                    .HasConstraintName("FK_Bookings_TimeSlots");
            });

            modelBuilder.Entity<Court>(entity =>
            {
                entity.Property(e => e.CourtId)
                    .HasMaxLength(50)
                    .HasColumnName("CourtID");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TimeSlotId).HasColumnName("TimeSlotID");

                entity.HasOne(d => d.TimeSlot)
                    .WithMany(p => p.Courts)
                    .HasForeignKey(d => d.TimeSlotId)
                    .HasConstraintName("FK_Courts_TimeSlots");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Dob)
                    .HasMaxLength(50)
                    .HasColumnName("DOB");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(50)
                    .HasColumnName("EmployeeID");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FullName).HasMaxLength(50);

                entity.Property(e => e.PasswordHash).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_Employees_Roles");
            });

            modelBuilder.Entity<ManagerCourt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CourtId)
                    .HasMaxLength(50)
                    .HasColumnName("CourtID");

                entity.Property(e => e.ManagerId)
                    .HasMaxLength(50)
                    .HasColumnName("ManagerID");

                entity.HasOne(d => d.Court)
                    .WithMany()
                    .HasForeignKey(d => d.CourtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerCourts_Courts");

                entity.HasOne(d => d.Manager)
                    .WithMany()
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManagerCourts_Employees");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.Property(e => e.PaymentId)
                    .HasMaxLength(50)
                    .HasColumnName("PaymentID");

                entity.Property(e => e.BookingId)
                    .HasMaxLength(50)
                    .HasColumnName("BookingID");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.PaymentMethod).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK_Payment_Bookings");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Payment_Customers");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId)
                    .ValueGeneratedNever()
                    .HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<StaffCourt>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.CourtId)
                    .HasMaxLength(50)
                    .HasColumnName("CourtID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(50)
                    .HasColumnName("StaffID");

                entity.HasOne(d => d.Court)
                    .WithMany()
                    .HasForeignKey(d => d.CourtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffCourts_Courts");

                entity.HasOne(d => d.Staff)
                    .WithMany()
                    .HasForeignKey(d => d.StaffId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StaffCourts_Employees");
            });

            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(e => e.TimeSlotsId);

                entity.Property(e => e.TimeSlotsId)
                    .ValueGeneratedNever()
                    .HasColumnName("TimeSlotsID");

                entity.Property(e => e.CourtId)
                    .HasMaxLength(50)
                    .HasColumnName("CourtID");

                entity.HasOne(d => d.Court)
                    .WithMany(p => p.TimeSlots)
                    .HasForeignKey(d => d.CourtId)
                    .HasConstraintName("FK_TimeSlots_Courts");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
