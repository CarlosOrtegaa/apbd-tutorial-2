namespace EquipmentRentalApp.Models;

public class Student : User
{
    public string StudentNumber { get; }

    public Student(string firstName, string lastName, string studentNumber) : base(firstName, lastName)
    {
        StudentNumber = studentNumber;
    }

    public override int RentalLimit => 2;
    public override string UserType => "Student";

    public override string GetDetails()
    {
        return $"{base.GetDetails()}, Student Number: {StudentNumber}";
    }
}