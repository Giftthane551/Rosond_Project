using System;
using Microsoft.EntityFrameworkCore;

namespace Rosond_Project.Models
{
    public partial class LawnMowingServiceDbContext : DbContext
    {
        // Constructor accepting DbContextOptions for dependency injection
        public LawnMowingServiceDbContext(DbContextOptions<LawnMowingServiceDbContext> options)
            : base(options)
        {
        }

        public  DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<ConflictManager> ConflictManagers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Machine> Machines { get; set; }
        public virtual DbSet<Operator> Operators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // This method should generally not be used when the context is configured via DI.
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951AEDCE26A21E");
                entity.Property(e => e.BookingId).ValueGeneratedNever();
                entity.Property(e => e.BookingDate).HasColumnType("datetime");
                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Customer).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Bookings__Custom__3D5E1FD2");

                entity.HasOne(d => d.Machine).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK__Bookings__Machin__3E52440B");

                entity.HasOne(d => d.Operator).WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.OperatorId)
                    .HasConstraintName("FK__Bookings__Operat__3F466844");
            });

            modelBuilder.Entity<ConflictManager>(entity =>
            {
                entity.HasKey(e => e.ConflictId).HasName("PK__Conflict__FEE84A369CE9913C");
                entity.ToTable("ConflictManager");
                entity.Property(e => e.ResolutionStatus).HasMaxLength(50);

                entity.HasOne(d => d.Booking).WithMany(p => p.ConflictManagers)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__ConflictM__Booki__440B1D61");

                entity.HasOne(d => d.NewMachine).WithMany(p => p.ConflictManagerNewMachines)
                    .HasForeignKey(d => d.NewMachineId)
                    .HasConstraintName("FK__ConflictM__NewMa__4316F928");

                entity.HasOne(d => d.OriginalMachine).WithMany(p => p.ConflictManagerOriginalMachines)
                    .HasForeignKey(d => d.OriginalMachineId)
                    .HasConstraintName("FK__ConflictM__Origi__4222D4EF");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CustomerId).HasName("PK__Customer__A4AE64D8B5A8BEB9");
                entity.Property(e => e.CustomerId).ValueGeneratedNever();
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FullName).HasMaxLength(100);
            });

            modelBuilder.Entity<Machine>(entity =>
            {
                entity.HasKey(e => e.MachineId).HasName("PK__Machines__44EE5B38817DFF5D");
                entity.Property(e => e.MachineId).ValueGeneratedNever();
                entity.Property(e => e.AvailabilityStatus).HasMaxLength(50);
                entity.Property(e => e.MachineName).HasMaxLength(100);
            });

            modelBuilder.Entity<Operator>(entity =>
            {
                entity.HasKey(e => e.OperatorId).HasName("PK__Operator__7BB12FAE279181C5");
                entity.Property(e => e.OperatorId).ValueGeneratedNever();
                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.HasOne(d => d.Machine).WithMany(p => p.Operators)
                    .HasForeignKey(d => d.MachineId)
                    .HasConstraintName("FK__Operators__Machi__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
