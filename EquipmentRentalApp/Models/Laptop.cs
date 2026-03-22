namespace EquipmentRentalApp.Models;

public class Laptop : Equipment
{
    public string Processor { get; }
    public int RamGb { get; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        Processor = processor;
        RamGb = ramGb;
    }

    public override string GetDetails()
    {
        return $"Laptop | Id: {Id}, Name: {Name}, Processor: {Processor}, RAM: {RamGb}GB, Status: {StatusNote}";
    }
}