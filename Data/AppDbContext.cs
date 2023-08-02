using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настраиваем связь многие-ко-многим между Note и Tag
        modelBuilder.Entity<Note>()
            .HasMany(n => n.Tags)
            .WithMany(t => t.Notes)
            .UsingEntity(j => j.ToTable("NoteTag"));
    }
}