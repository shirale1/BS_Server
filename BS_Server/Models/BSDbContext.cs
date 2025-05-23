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

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Recommendation> Recommendations { get; set; }

    public virtual DbSet<Tip> Tips { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = (localdb)\\MSSQLLocalDB;Initial Catalog=BS_DB;User ID=Login;Password=shira123;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Babysiter>(entity =>
        {
            entity.HasKey(e => e.BabysiterId).HasName("PK__Babysite__E9AD8FB1A9492ED4");

            entity.Property(e => e.BabysiterId).ValueGeneratedNever();

            entity.HasOne(d => d.BabysiterNavigation).WithOne(p => p.Babysiter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Babysiters");
        });

        modelBuilder.Entity<Parent>(entity =>
        {
            entity.HasKey(e => e.ParentId).HasName("PK__Parents__D339516F7B3FF3DA");

            entity.Property(e => e.ParentId).ValueGeneratedNever();

            entity.HasOne(d => d.ParentNavigation).WithOne(p => p.Parent)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Parents");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__Rating__FCCDF87CAAB88A33");

            entity.Property(e => e.RatingValue).HasDefaultValue(0);

            entity.HasOne(d => d.User).WithMany(p => p.Ratings).HasConstraintName("FK__Rating__UserId__2E1BDC42");
        });

        modelBuilder.Entity<Recommendation>(entity =>
        {
            entity.HasKey(e => e.RecommendationId).HasName("PK__Recommen__AA15BEE43EF38ED1");

            entity.HasOne(d => d.User).WithMany(p => p.Recommendations).HasConstraintName("FK__Recommend__UserI__31EC6D26");
        });

        modelBuilder.Entity<Tip>(entity =>
        {
            entity.HasKey(e => e.TipId).HasName("PK__Tips__2DB1A1C85169FAE3");

            entity.HasOne(d => d.User).WithMany(p => p.Tips).HasConstraintName("FK__Tips__UserId__34C8D9D1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3213E83F4DCBB3FF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
