﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PC118100439CANO.DOMAIN.Core.Entities;

namespace PC118100439CANO.DOMAIN.Core.Infrastructure.Data;

public partial class EventManagementDbContext : DbContext
{
    public EventManagementDbContext()
    {
    }

    public EventManagementDbContext(DbContextOptions<EventManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendees> Attendees { get; set; }

    public virtual DbSet<EventAttendance> EventAttendance { get; set; }

    public virtual DbSet<Events> Events { get; set; }

    public virtual DbSet<Organizers> Organizers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendees>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Attendee__3214EC07AB839796");

            entity.Property(e => e.AttendeeName).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<EventAttendance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EventAtt__3214EC07EFE13539");

            entity.Property(e => e.CheckedInAt).HasColumnType("datetime");

            entity.HasOne(d => d.Attendee).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.AttendeeId)
                .HasConstraintName("FK__EventAtte__Atten__2D27B809");

            entity.HasOne(d => d.Event).WithMany(p => p.EventAttendance)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__EventAtte__Event__2E1BDC42");
        });

        modelBuilder.Entity<Events>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Events__3214EC070905D548");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EventDate).HasColumnType("datetime");
            entity.Property(e => e.EventName).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(200);

            entity.HasOne(d => d.Organizer).WithMany(p => p.Events)
                .HasForeignKey(d => d.OrganizerId)
                .HasConstraintName("FK__Events__Organize__2F10007B");
        });

        modelBuilder.Entity<Organizers>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Organize__3214EC07F3BB6961");

            entity.Property(e => e.ContactEmail).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrganizerName).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
