namespace EquipmentRentalApp.Models;

public abstract class User
{
    private static int _nextId = 1;

    public int Id { get; }
    public string FirstName { get; }
    public string LastName { get; }

    protected User(string firstName, string lastName)
    {
        Id = _nextId++;
        FirstName = firstName;
        LastName = lastName;
    }

    public string FullName => $"{FirstName} {LastName}";

    public abstract int RentalLimit { get; }
    public abstract string UserType { get; }

    public virtual string GetDetails()
    {
        return $"{UserType} | Id: {Id}, Name: {FullName}, Limit: {RentalLimit}";
    }
}