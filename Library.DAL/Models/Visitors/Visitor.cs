using Library.DAL.Models.Statistic;

namespace Library.DAL.Models.Visitors;

public class Visitor
{
    public Visitor() { }

    public Visitor(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<RentedBook> RentedBooks { get; } = [];

    public List<Violation> Violations { get; } = [];
}
