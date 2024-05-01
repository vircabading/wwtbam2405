using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Data;

public partial class KsjvContext : IdentityDbContext<User>
{


    public KsjvContext(DbContextOptions<KsjvContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Game> Games { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Games__2AB897FD61179E10");

            entity.ToTable("Games", "p2");

            entity.Property(e => e.PlayedAt).HasColumnType("datetime");

            // entity.HasOne(d => d.User).WithMany(p => p.Games)
            //     .HasForeignKey(d => d.UserId)
            //     .HasConstraintName("FK__Games__UserId__6383C8BA");
        });

        // modelBuilder.Entity<User>(entity =>
        // {
        //     entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CDFB466B0");

        //     entity.ToTable("Users", "p2");

        //     entity.Property(e => e.Username).HasMaxLength(20);
        // });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
