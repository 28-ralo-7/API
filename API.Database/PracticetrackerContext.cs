using Microsoft.EntityFrameworkCore;

namespace API.Database;

public partial class PracticetrackerContext : DbContext
{
    public PracticetrackerContext()
    {
    }

    public PracticetrackerContext(DbContextOptions<PracticetrackerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Practice> Practices { get; set; }

    public virtual DbSet<Practicelog> Practicelogs { get; set; }

    public virtual DbSet<Practiceschedule> Practiceschedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=practicetracker;Username=postgres;Password=1");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Company>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isremoved)
                .HasDefaultValue(false)
                .HasColumnName("isremoved");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("group");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isremoved)
                .HasDefaultValue(false)
                .HasColumnName("isremoved");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Practice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("practice");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isremoved)
                .HasDefaultValue(false)
                .HasColumnName("isremoved");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
        });

        modelBuilder.Entity<Practicelog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("practicelog");

            entity.Property(e => e.Companyid).HasColumnName("companyid");
            entity.Property(e => e.Contract)
                .HasColumnType("character varying")
                .HasColumnName("contract");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Practicescheduleid).HasColumnName("practicescheduleid");
            entity.Property(e => e.Userid).HasColumnName("userid");
        });

        modelBuilder.Entity<Practiceschedule>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("practiceschedule");

            entity.Property(e => e.Dateend).HasColumnName("dateend");
            entity.Property(e => e.Datestart).HasColumnName("datestart");
            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Practiceleadid).HasColumnName("practiceleadid");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isremoved)
                .HasDefaultValue(false)
                .HasColumnName("isremoved");
            entity.Property(e => e.Practiceid).HasColumnName("practiceid");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("users");

            entity.Property(e => e.Groupid).HasColumnName("groupid");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Isremoved)
                .HasDefaultValue(false)
                .HasColumnName("isremoved");
            entity.Property(e => e.Login)
                .HasColumnType("character varying")
                .HasColumnName("login");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Passwordhash)
                .HasColumnType("character varying")
                .HasColumnName("passwordhash");
            entity.Property(e => e.Patronomic)
                .HasColumnType("character varying")
                .HasColumnName("patronomic");
            entity.Property(e => e.Roletype).HasColumnName("roletype");
            entity.Property(e => e.Surname)
                .HasColumnType("character varying")
                .HasColumnName("surname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
