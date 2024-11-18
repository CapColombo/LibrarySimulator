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
        RoleId = role.Id;
        WorkSchedule = workSchedule;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid RoleId { get; set; }

    public Role Role { get; set; }

    public WorkSchedule WorkSchedule { get; set; }
}
