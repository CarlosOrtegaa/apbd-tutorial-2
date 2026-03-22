namespace EquipmentRentalApp.Models;

public abstract class Equipment
{
    private static int _nextId = 1;

    public int Id { get; }
    public string Name { get; }
    public bool IsAvailable { get; private set; }
    public string StatusNote { get; private set; }

    protected Equipment(string name)
    {
        Id = _nextId++;
        Name = name;
        IsAvailable = true;
        StatusNote = "Available";
    }

    public void MarkAsRented()
    {
        IsAvailable = false;
        StatusNote = "Currently rented";
    }

    public void MarkAsReturned()
    {
        IsAvailable = true;
        StatusNote = "Available";
    }

    public void MarkAsUnavailable(string reason)
    {
        IsAvailable = false;
        StatusNote = reason;
    }

    public abstract string GetDetails();
}