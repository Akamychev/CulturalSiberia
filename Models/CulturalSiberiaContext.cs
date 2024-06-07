using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CulturalSiberiaProject.Models;

public partial class CulturalSiberiaContext : DbContext
{
    public CulturalSiberiaContext()
    {
    }

    public CulturalSiberiaContext(DbContextOptions<CulturalSiberiaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Cinema> Cinemas { get; set; }

    public virtual DbSet<Concert> Concerts { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Museum> Museums { get; set; }

    public virtual DbSet<Showpiece> Showpieces { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Usersrole> Usersroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=Cultural_Siberia;Username=postgres;Password=62......w");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("artist_pkey");

            entity.ToTable("artist");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Deathdate).HasColumnName("deathdate");
            entity.Property(e => e.Fname)
                .HasColumnType("character varying")
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasColumnType("character varying")
                .HasColumnName("lname");
            entity.Property(e => e.Mname)
                .HasColumnType("character varying")
                .HasColumnName("mname");
            entity.Property(e => e.Works)
                .HasColumnType("character varying")
                .HasColumnName("works");
        });

        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cinema_pkey");

            entity.ToTable("cinema");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Artistsid).HasColumnName("artistsid");
            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.Contry)
                .HasColumnType("character varying")
                .HasColumnName("contry");
            entity.Property(e => e.Genre)
                .HasColumnType("character varying")
                .HasColumnName("genre");
            entity.Property(e => e.Languages)
                .HasColumnType("character varying")
                .HasColumnName("languages");
            entity.Property(e => e.Nameing)
                .HasColumnType("character varying")
                .HasColumnName("nameing");
            entity.Property(e => e.Realisedate).HasColumnName("realisedate");
            entity.Property(e => e.Runningtime)
                .HasColumnType("character varying")
                .HasColumnName("runningtime");
            entity.Property(e => e.Studio)
                .HasColumnType("character varying")
                .HasColumnName("studio");

            entity.HasOne(d => d.Artists).WithMany(p => p.Cinemas)
                .HasForeignKey(d => d.Artistsid)
                .HasConstraintName("cinema_artistsid_fkey");
        });

        modelBuilder.Entity<Concert>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("concert_pkey");

            entity.ToTable("concert");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Artistsid).HasColumnName("artistsid");
            entity.Property(e => e.Concertdate).HasColumnName("concertdate");
            entity.Property(e => e.Duration).HasColumnName("duration");
            entity.Property(e => e.Numberofseats).HasColumnName("numberofseats");
            entity.Property(e => e.Programofconcert)
                .HasColumnType("character varying")
                .HasColumnName("programofconcert");

            entity.HasOne(d => d.Artists).WithMany(p => p.Concerts)
                .HasForeignKey(d => d.Artistsid)
                .HasConstraintName("concert_artistsid_fkey");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("events_pkey");

            entity.ToTable("events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cinemaid).HasColumnName("cinemaid");
            entity.Property(e => e.Concertid).HasColumnName("concertid");
            entity.Property(e => e.Isfavorite).HasColumnName("isfavorite");
            entity.Property(e => e.Museumid).HasColumnName("museumid");

            entity.HasOne(d => d.Cinema).WithMany(p => p.Events)
                .HasForeignKey(d => d.Cinemaid)
                .HasConstraintName("events_cinemaid_fkey");

            entity.HasOne(d => d.Concert).WithMany(p => p.Events)
                .HasForeignKey(d => d.Concertid)
                .HasConstraintName("events_concertid_fkey");

            entity.HasOne(d => d.Museum).WithMany(p => p.Events)
                .HasForeignKey(d => d.Museumid)
                .HasConstraintName("events_museumid_fkey");
        });

        modelBuilder.Entity<Museum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("museum_pkey");

            entity.ToTable("museum");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Foundationdate).HasColumnName("foundationdate");
            entity.Property(e => e.Founder)
                .HasColumnType("character varying")
                .HasColumnName("founder");
            entity.Property(e => e.Hisory)
                .HasColumnType("character varying")
                .HasColumnName("hisory");
            entity.Property(e => e.Hoursofworks)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("hoursofworks");
            entity.Property(e => e.Nameofmuseum)
                .HasColumnType("character varying")
                .HasColumnName("nameofmuseum");
            entity.Property(e => e.Showpieces).HasColumnName("showpieces");

            entity.HasOne(d => d.ShowpiecesNavigation).WithMany(p => p.Museums)
                .HasForeignKey(d => d.Showpieces)
                .HasConstraintName("museum_showpieces_fkey");
        });

        modelBuilder.Entity<Showpiece>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("showpiece_pkey");

            entity.ToTable("showpiece");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Borndate).HasColumnName("borndate");
            entity.Property(e => e.History)
                .HasColumnType("character varying")
                .HasColumnName("history");
            entity.Property(e => e.Nameing)
                .HasColumnType("character varying")
                .HasColumnName("nameing");
            entity.Property(e => e.Originality).HasColumnName("originality");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Subject)
                .HasColumnType("character varying")
                .HasColumnName("subject");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ticket_pkey");

            entity.ToTable("ticket");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Eventdate).HasColumnName("eventdate");
            entity.Property(e => e.Eventid).HasColumnName("eventid");
            entity.Property(e => e.Eventname)
                .HasColumnType("character varying")
                .HasColumnName("eventname");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Event).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Eventid)
                .HasConstraintName("ticket_eventid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("ticket_userid_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasColumnType("character varying")
                .HasColumnName("email");
            entity.Property(e => e.Fname)
                .HasColumnType("character varying")
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasColumnType("character varying")
                .HasColumnName("lname");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Mname)
                .HasColumnType("character varying")
                .HasColumnName("mname");
            entity.Property(e => e.Userpassword)
                .HasColumnType("character varying")
                .HasColumnName("userpassword");
            entity.Property(e => e.Userrole)
                .HasColumnType("character varying")
                .HasColumnName("userrole");

            entity.HasOne(d => d.UserroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Userrole)
                .HasConstraintName("users_userrole_fkey");
        });

        modelBuilder.Entity<Usersrole>(entity =>
        {
            entity.HasKey(e => e.Roles).HasName("usersroles_pkey");

            entity.ToTable("usersroles");

            entity.Property(e => e.Roles)
                .HasColumnType("character varying")
                .HasColumnName("roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
