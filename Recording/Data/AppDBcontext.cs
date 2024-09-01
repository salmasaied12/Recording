using Microsoft.EntityFrameworkCore;
using Recording.Model;

public class AppDBContext : DbContext
{
    public DbSet<AudioRecording> AudioRecordings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=your_database_file.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AudioRecording>().ToTable("AudioRecordings");
    }
}

