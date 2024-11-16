namespace Library.DAL.Models.Employees;

public class Role
{
    public Role() { }

    public Role(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; }
    public string Name { get; }
}
