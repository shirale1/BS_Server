using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BS_Server.Models;

public partial class BSDbContext : DbContext
{
    public BSDbContext()
    {
    }

    public BSDbContext(DbContextOptions<BSDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Babysiter> Babysiters { get; set; }

    public virtual DbSet<Parent> Parents { get; set; }

    public virtual DbSet<StatusTable> StatusTables { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<WaitingLb> WaitingLbs { get; set; }

    public virtual DbSet<WaitingLp> WaitingLps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Babysiter>(entity =>
        {
            entity.HasKey(e => e.BabysiterId).HasName("PK__Babysite__E9AD8FB1216DFBE4");

            entity.Property(e => e.BabysiterId).ValueGeneratedNever();

            entity.HasOne(d => d.BabysiterNavigation).WithOne(p => p.Babysiter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Babysiters");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__Parents__D339516F5FFFD9E1");

            entity.Property(e => e.ParentId).ValueGeneratedNever();

            entity.HasOne(d => d.ParentNavigation).WithOne(p => p.Parent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parents");
        });

        modelBuilder.Entity<StatusTable>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__StatusTa__C8EE2063FF1F9FD7");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F912B1D02");
        });

        modelBuilder.Entity<WaitingLb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WaitingL__3214EC07F5AB82B8");

            entity.HasOne(d => d.Babysiter).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Babys__34C8D9D1");

            entity.HasOne(d => d.Parent).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Paren__33D4B598");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Statu__35BCFE0A");
        });

        modelBuilder.Entity<WaitingLp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WaitingL__3214EC07340A7994");

            entity.HasOne(d => d.Babysiter).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Babys__300424B4");

            entity.HasOne(d => d.Parent).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Paren__2F10007B");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Statu__30F848ED");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
