namespace EquipmentRentalApp.Models;

public class Employee : User
{
    public string Department { get; }

    public Employee(string firstName, string lastName, string department) : base(firstName, lastName)
    {
        Department = department;
    }

    public override int RentalLimit => 5;
    public override string UserType => "Employee";

    public override string GetDetails()
    {
        return $"{base.GetDetails()}, Department: {Department}";
    }
}