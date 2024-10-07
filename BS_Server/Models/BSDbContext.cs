﻿using System;
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

    public virtual DbSet<Supervisor> Supervisors { get; set; }

    public virtual DbSet<WaitingLb> WaitingLbs { get; set; }

    public virtual DbSet<WaitingLp> WaitingLps { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Babysiter>(entity =>
        {
            entity.HasKey(e => e.BabysiterId).HasName("PK__Babysite__E9AD8FB1F8F12D23");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__Parents__D339516FCD62B1F9");
        });

        modelBuilder.Entity<StatusTable>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__StatusTa__C8EE2063C382A34A");
        });

        modelBuilder.Entity<Supervisor>(entity =>
        {
            entity.HasKey(e => e.SupervisorId).HasName("PK__Supervis__6FAABDCFD7586D78");
        });

        modelBuilder.Entity<WaitingLb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WaitingL__3214EC071AD4A999");

            entity.HasOne(d => d.Babysiter).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Babys__31EC6D26");

            entity.HasOne(d => d.Parent).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Paren__30F848ED");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.WaitingLbs).HasConstraintName("FK__WaitingLB__Statu__32E0915F");
        });

        modelBuilder.Entity<WaitingLp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__WaitingL__3214EC075C180F95");

            entity.HasOne(d => d.Babysiter).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Babys__2D27B809");

            entity.HasOne(d => d.Parent).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Paren__2C3393D0");

            entity.HasOne(d => d.StatusCodeNavigation).WithMany(p => p.WaitingLps).HasConstraintName("FK__WaitingLP__Statu__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}