namespace EquipmentRentalApp.Models;

public class Rental
{
    public User User { get; }
    public Equipment Equipment { get; }
    public DateTime RentalDate { get; }
    public int RentalDays { get; }
    public DateTime DueDate => RentalDate.AddDays(RentalDays);
    public DateTime? ReturnDate { get; private set; }
    public decimal Penalty { get; private set; }

    public bool IsReturned => ReturnDate.HasValue;
    public bool IsActive => !IsReturned;
    public bool IsOverdue => !IsReturned && DateTime.Now.Date > DueDate.Date;
    public bool WasReturnedLate => ReturnDate is not null && ReturnDate.Value.Date > DueDate.Date;

    public Rental(User user, Equipment equipment, DateTime rentalDate, int rentalDays)
    {
        User = user;
        Equipment = equipment;
        RentalDate = rentalDate;
        RentalDays = rentalDays;
        Penalty = 0;
    }

    public void Return(DateTime returnDate, decimal dailyPenaltyRate)
    {
        if (IsReturned)
        {
            throw new InvalidOperationException("This rental has already been returned.");
        }

        ReturnDate = returnDate;

        int lateDays = (ReturnDate.Value.Date - DueDate.Date).Days;
        if (lateDays > 0)
        {
            Penalty = lateDays * dailyPenaltyRate;
        }
    }

    public string GetDetails()
    {
        string returnInfo = IsReturned
            ? $"Returned: {ReturnDate:yyyy-MM-dd}, Penalty: {Penalty:C}"
            : "Not returned yet";

        return $"Rental | User: {User.FullName}, Equipment: {Equipment.Name}, Rental Date: {RentalDate:yyyy-MM-dd}, Due Date: {DueDate:yyyy-MM-dd}, {returnInfo}";
    }
}