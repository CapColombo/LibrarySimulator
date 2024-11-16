using Library.DAL.Models.Enums;

namespace Library.DAL.Models.Employees;

public class Employee
{
    public Employee() { }

    public Employee(string name, Role role, WorkSchedule workSchedule)
    {
        Id = Guid.NewGuid();
        Name = name;
        Role = role;
        WorkSchedule = workSchedule;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public Role Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }
}
