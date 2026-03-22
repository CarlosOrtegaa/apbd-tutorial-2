namespace EquipmentRentalApp.Models;

public class Projector : Equipment
{
    public int Lumens { get; }
    public double WeightKg { get; }

    public Projector(string name, int lumens, double weightKg) : base(name)
    {
        Lumens = lumens;
        WeightKg = weightKg;
    }

    public override string GetDetails()
    {
        return $"Projector | Id: {Id}, Name: {Name}, Lumens: {Lumens}, Weight: {WeightKg}kg, Status: {StatusNote}";
    }
}