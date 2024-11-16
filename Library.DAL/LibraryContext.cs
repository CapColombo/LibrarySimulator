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
    }
}
