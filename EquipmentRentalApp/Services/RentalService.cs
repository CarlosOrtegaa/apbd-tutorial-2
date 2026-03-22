using EquipmentRentalApp.Models;

namespace EquipmentRentalApp.Services;

public class RentalService
{
    private const decimal DailyPenaltyRate = 10m;

    private readonly List<Equipment> _equipmentItems = new();
    private readonly List<User> _users = new();
    private readonly List<Rental> _rentals = new();

    public void AddUser(User user)
    {
        _users.Add(user);
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipmentItems.Add(equipment);
    }

    public List<Equipment> GetAllEquipment()
    {
        return _equipmentItems;
    }

    public List<Equipment> GetAvailableEquipment()
    {
        return _equipmentItems.Where(e => e.IsAvailable).ToList();
    }

    public List<Rental> GetAllRentals()
    {
        return _rentals;
    }

    public Rental RentEquipment(int userId, int equipmentId, int rentalDays)
    {
        User? user = _users.FirstOrDefault(u => u.Id == userId);
        if (user is null)
        {
            throw new InvalidOperationException("User not found.");
        }

        Equipment? equipment = _equipmentItems.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment is null)
        {
            throw new InvalidOperationException("Equipment not found.");
        }

        if (!equipment.IsAvailable)
        {
            throw new InvalidOperationException("Equipment is not available for rental.");
        }

        int activeRentalsCount = _rentals.Count(r => r.User.Id == userId && r.IsActive);
        if (activeRentalsCount >= user.RentalLimit)
        {
            throw new InvalidOperationException("User has exceeded the rental limit.");
        }

        Rental rental = new Rental(user, equipment, DateTime.Now.Date, rentalDays);
        _rentals.Add(rental);
        equipment.MarkAsRented();

        return rental;
    }

    public Rental RentEquipmentWithDate(int userId, int equipmentId, int rentalDays, DateTime rentalDate)
    {
        User? user = _users.FirstOrDefault(u => u.Id == userId);
        if (user is null)
        {
            throw new InvalidOperationException("User not found.");
        }

        Equipment? equipment = _equipmentItems.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment is null)
        {
            throw new InvalidOperationException("Equipment not found.");
        }

        if (!equipment.IsAvailable)
        {
            throw new InvalidOperationException("Equipment is not available for rental.");
        }

        int activeRentalsCount = _rentals.Count(r => r.User.Id == userId && r.IsActive);
        if (activeRentalsCount >= user.RentalLimit)
        {
            throw new InvalidOperationException("User has exceeded the rental limit.");
        }

        Rental rental = new Rental(user, equipment, rentalDate, rentalDays);
        _rentals.Add(rental);
        equipment.MarkAsRented();

        return rental;
    }

    public void ReturnEquipment(int equipmentId, DateTime returnDate)
    {
        Rental? rental = _rentals.FirstOrDefault(r => r.Equipment.Id == equipmentId && r.IsActive);
        if (rental is null)
        {
            throw new InvalidOperationException("Active rental for this equipment was not found.");
        }

        rental.Return(returnDate, DailyPenaltyRate);
        rental.Equipment.MarkAsReturned();
    }

    public void MarkEquipmentAsUnavailable(int equipmentId, string reason)
    {
        Equipment? equipment = _equipmentItems.FirstOrDefault(e => e.Id == equipmentId);
        if (equipment is null)
        {
            throw new InvalidOperationException("Equipment not found.");
        }

        if (_rentals.Any(r => r.Equipment.Id == equipmentId && r.IsActive))
        {
            throw new InvalidOperationException("Cannot mark rented equipment as unavailable.");
        }

        equipment.MarkAsUnavailable(reason);
    }

    public List<Rental> GetActiveRentalsForUser(int userId)
    {
        return _rentals.Where(r => r.User.Id == userId && r.IsActive).ToList();
    }

    public List<Rental> GetOverdueRentals()
    {
        return _rentals.Where(r => r.IsOverdue).ToList();
    }

    public string GetSummaryReport()
    {
        int totalEquipment = _equipmentItems.Count;
        int availableEquipment = _equipmentItems.Count(e => e.IsAvailable);
        int unavailableEquipment = totalEquipment - availableEquipment;
        int activeRentals = _rentals.Count(r => r.IsActive);
        int overdueRentals = _rentals.Count(r => r.IsOverdue);
        decimal totalPenalties = _rentals.Sum(r => r.Penalty);

        return
            $"Summary Report\n" +
            $"Total equipment: {totalEquipment}\n" +
            $"Available equipment: {availableEquipment}\n" +
            $"Unavailable equipment: {unavailableEquipment}\n" +
            $"Active rentals: {activeRentals}\n" +
            $"Overdue rentals: {overdueRentals}\n" +
            $"Total penalties: {totalPenalties:C}";
    }
}