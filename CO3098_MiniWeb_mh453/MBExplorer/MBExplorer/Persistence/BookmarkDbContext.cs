using MBExplorer.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace MBExplorer.Persistence
{
    public class BookmarkDbContext : DbContext
    {
        public DbSet<Bookmark> Bookmarks { get; set; }

        public BookmarkDbContext(DbContextOptions<BookmarkDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bookmark>().Property(b => b.Name).HasMaxLength(250);
            builder.Entity<ItemLink>().Property(i => i.URL).HasMaxLength(3000);
            builder.Entity<Folder>();
            builder.Entity<ItemLocation>();
            builder.Entity<ItemTextFile>();

            base.OnModelCreating(builder);
        }
    }
}
