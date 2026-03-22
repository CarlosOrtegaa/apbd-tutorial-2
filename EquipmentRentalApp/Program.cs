using EquipmentRentalApp.Models;
using EquipmentRentalApp.Services;

RentalService rentalService = new RentalService();

Student student = new Student("Alice", "Johnson", "S12345");
Employee employee = new Employee("Bob", "Smith", "IT");

Laptop laptop = new Laptop("Dell XPS 15", "Intel i7", 16);
Projector projector = new Projector("Epson X200", 3500, 2.8);
Camera camera = new Camera("Canon EOS", "24MP", true);
Laptop secondLaptop = new Laptop("Lenovo ThinkPad", "Intel i5", 8);
Camera secondCamera = new Camera("Nikon D3500", "20MP", false);

rentalService.AddUser(student);
rentalService.AddUser(employee);

rentalService.AddEquipment(laptop);
rentalService.AddEquipment(projector);
rentalService.AddEquipment(camera);
rentalService.AddEquipment(secondLaptop);
rentalService.AddEquipment(secondCamera);

Console.WriteLine("ALL EQUIPMENT:");
foreach (Equipment item in rentalService.GetAllEquipment())
{
    Console.WriteLine(item.GetDetails());
}

Console.WriteLine("\nCORRECT RENTAL:");
Rental rental1 = rentalService.RentEquipment(student.Id, laptop.Id, 5);
Console.WriteLine(rental1.GetDetails());

Console.WriteLine("\nRETURN ON TIME:");
rentalService.ReturnEquipment(laptop.Id, DateTime.Now.Date.AddDays(5));
Console.WriteLine("Laptop returned on time.");

Console.WriteLine("\nDELAYED RETURN WITH PENALTY:");
Rental delayedRental = rentalService.RentEquipment(employee.Id, projector.Id, 2);
rentalService.ReturnEquipment(projector.Id, DateTime.Now.Date.AddDays(5));
Console.WriteLine(delayedRental.GetDetails());

Console.WriteLine("\nEXCEED LIMIT ATTEMPT:");
try
{
    rentalService.RentEquipment(student.Id, camera.Id, 3);
    rentalService.RentEquipment(student.Id, secondLaptop.Id, 3);
    rentalService.RentEquipment(student.Id, secondCamera.Id, 3);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\nACTIVE RENTALS FOR STUDENT:");
foreach (Rental rental in rentalService.GetActiveRentalsForUser(student.Id))
{
    Console.WriteLine(rental.GetDetails());
}

Console.WriteLine("\nMARK EQUIPMENT AS UNAVAILABLE:");
try
{
    rentalService.MarkEquipmentAsUnavailable(secondCamera.Id, "Under maintenance");
    Console.WriteLine(secondCamera.GetDetails());
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

Console.WriteLine("\nOVERDUE RENTALS:");
Rental overdueRental = rentalService.RentEquipmentWithDate(employee.Id, laptop.Id, 3, DateTime.Now.Date.AddDays(-7));
foreach (Rental rental in rentalService.GetOverdueRentals())
{
    Console.WriteLine(rental.GetDetails());
}

Console.WriteLine("\nFINAL REPORT:");
Console.WriteLine(rentalService.GetSummaryReport());