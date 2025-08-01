using leave_it_small.Models;
using leave_it_small.utils;
using Microsoft.EntityFrameworkCore;

namespace leave_it_small.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ShortenedUrl>(builder =>
        {
            builder 
                .Property(shortenedUrl => shortenedUrl.Code)
                .HasMaxLength(ShortLinkSettings.Length);

            builder
                .HasIndex(ShortenedUrl => ShortenedUrl.Code)
                .IsUnique();
        });
    }
}