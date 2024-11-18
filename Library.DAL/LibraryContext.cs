using Library.DAL.Models.Books;
using Library.DAL.Models.Employees;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Library.DAL;

public class LibraryContext : DbContext, ILibraryContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Author> Authors { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Employee> Employees { get; set; }

    public DbSet<Visitor> Visitors { get; set; }

    public DbSet<RentedBook> RentedBooks { get; set; }

    public DbSet<Operation> Operations { get; set; }

    public DbSet<Violation> Violations { get; set; }

    public async Task<int> AddWithSaveAsync<T>(T value, CancellationToken cancellationToken) where T : class
    {
        Add(value);
        return await SaveChangesAsync(cancellationToken);
    }

    public async Task<int> RemoveWithSaveAsync<T>(T value, CancellationToken token) where T : class
    {
        Remove(value);
        return await SaveChangesAsync(token);
    }

    public async Task<int> UpdateWithSaveAsync<T>(T value, CancellationToken cancellationToken) where T : class
    {
        Update(value);
        return await SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>(b =>
        { 
            b.HasKey(e => e.Id);
            b.HasMany(e => e.Books).WithMany(b => b.Authors);
        });

        modelBuilder.Entity<Book>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasMany(e => e.Authors).WithMany(b => b.Books);
            b.HasMany(e => e.Genres);
            b.HasOne(e => e.RentedBook).WithOne(e => e.Book).HasForeignKey<RentedBook>(e => e.BookId);
        });

        modelBuilder.Entity<RentedBook>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasOne(e => e.Book).WithOne(e => e.RentedBook).HasForeignKey<Book>(e => e.RentedBookId);
            b.HasOne(e => e.Visitor).WithMany(e => e.RentedBooks).HasForeignKey(e => e.VisitorId);
        });

        modelBuilder.Entity<Visitor>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasMany(e => e.RentedBooks).WithOne(e => e.Visitor);
            b.HasMany(e => e.Violations).WithOne(e => e.Visitor);
        });

        modelBuilder.Entity<Violation>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasOne(e => e.Book);
            b.HasOne(e => e.Visitor).WithMany(e => e.Violations).HasForeignKey(e => e.VisitorId);
        });

        modelBuilder.Entity<Employee>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasOne(e => e.Role);
        });

        modelBuilder.Entity<Role>(b =>
        {
            b.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Operation>(b =>
        {
            b.HasKey(e => e.Id);
            b.HasOne(e => e.Book);
            b.HasOne(e => e.Visitor);
            b.HasIndex(e => e.Date);
        });
    }
}
