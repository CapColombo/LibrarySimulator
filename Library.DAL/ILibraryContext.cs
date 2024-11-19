using Library.DAL.Models.Books;
using Library.DAL.Models.Employees;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Library.DAL;

public interface ILibraryContext
{
    DbSet<Author> Authors { get; set; }

    DbSet<Book> Books { get; set; }

    DbSet<Role> Roles { get; set; }

    DbSet<Employee> Employees { get; set; }

    DbSet<Visitor> Visitors { get; set; }

    DbSet<RentedBook> RentedBooks { get; set; }

    DbSet<Operation> Operations { get; set; }

    DbSet<Violation> Violations { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);

    ValueTask<EntityEntry<T>> AddAsync<T>(T value, CancellationToken token = default) where T : class;

    EntityEntry<T> Update<T>(T value) where T : class;

    EntityEntry<T> Remove<T>(T value) where T : class;

    public void UpdateRange(params object[] entities);
}