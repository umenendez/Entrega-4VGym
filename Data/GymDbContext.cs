using _4VGymEF.Models;
using Microsoft.EntityFrameworkCore;

namespace _4VGymEF.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext> options)
            : base(options)
        {
        }

        // Cada DbSet se mapea a una tabla de SQL Server
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- ActivityType ----------
            modelBuilder.Entity<ActivityType>(entity =>
            {
                entity.ToTable("ActivityTypes");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.NumberMonitors)
                      .HasDefaultValue(1);

                // No puede haber dos tipos de actividad con el mismo nombre
                entity.HasIndex(e => e.Name)
                      .IsUnique();

                // Relación 1:N (ActivityType -> Activities)
                entity.HasMany(e => e.Activities)
                      .WithOne(a => a.ActivityType)
                      .HasForeignKey(a => a.ActivityTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // ---------- Instructor ----------
            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructors");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(e => e.PhotoUrl)
                      .HasMaxLength(500);

                entity.Property(e => e.Phone)
                      .HasMaxLength(20);

                // No puede haber dos monitores con el mismo email y nombre
                entity.HasIndex(e => new { e.Email, e.Name })
                      .IsUnique();
            });

            // ---------- Activity ----------
            modelBuilder.Entity<Activity>(entity =>
            {
                entity.ToTable("Activities", table =>
                {
                    table.HasCheckConstraint("CK_Activities_MaxParticipants", "[MaxParticipants] BETWEEN 1 AND 25");
                    table.HasCheckConstraint("CK_Activities_Dates", "[DateEnd] > [DateStart]");
                });

                entity.HasKey(e => e.Id);

                // Relación M:N (Activity <-> Instructor) con tabla intermedia
                entity.HasMany(e => e.Instructors)
                      .WithMany(i => i.Activities)
                      .UsingEntity(join => join.ToTable("ActivityInstructor"));
            });

            // ---------- Datos semilla ----------
            // Los IDs son fijos para que las migraciones sean deterministas.
            modelBuilder.Entity<ActivityType>().HasData(
                new ActivityType { Id = 1, Name = "BodyPump", NumberMonitors = 2 },
                new ActivityType { Id = 2, Name = "Spinning", NumberMonitors = 1 },
                new ActivityType { Id = 3, Name = "Pilates", NumberMonitors = 1 },
                new ActivityType { Id = 4, Name = "CrossFit", NumberMonitors = 3 }
            );

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    Id = 1,
                    Name = "Miguel Goyena",
                    Email = "miguel_goyena@cuatrovientos.org",
                    Phone = "654121212",
                    PhotoUrl = "https://img.magnific.com/vector-premium/imagen-perfil-avatar-hombre-aislada-fondo-imagen-profil-avatar-hombre_1293239-4841.jpg?semt=ais_hybrid&w=740&q=80"
                },
                new Instructor
                {
                    Id = 2,
                    Name = "María Martín",
                    Email = "maria_martin@cuatrovientos.org",
                    Phone = "663423121",
                    PhotoUrl = "https://img.magnific.com/vector-premium/avatar-mujer-sonriente_937492-6135.jpg?semt=ais_hybrid&w=740&q=80"
                },
                new Instructor
                {
                    Id = 3,
                    Name = "Iban Sarría",
                    Email = "iban_sarria@cuatrovientos.org",
                    Phone = "654646464",
                    PhotoUrl = "https://img.magnific.com/vector-premium/imagen-perfil-avatar-hombre-aislada-fondo-imagen-profil-avatar-hombre_1293239-4861.jpg?semt=ais_hybrid&w=740&q=80"
                }
            );
        }
    }
}
