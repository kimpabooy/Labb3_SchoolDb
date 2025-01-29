using Labb3_SchoolDb.Data;
using Labb3_SchoolDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_SchoolDb
{
    public class DatabaseManager
    {
        public void GetAllStudents()
        {
            using (var context = new SchoolDbContext())
            {
                bool ascending = false;
                bool ok = false;
                string sortBy = "";
                string userChoiceAcending = "";

                while (!ok)
                {

                    // Asking user in what order to sort the students.
                    while (true)
                    {
                        Console.WriteLine("Sortera på (N)amn eller (E)fternamn?");
                        sortBy = Console.ReadLine().ToLower();

                        // Checks if the input is correct
                        if (sortBy == "n" || sortBy == "e")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt val. Vänligen välj 'N' eller 'E'.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }

                    // Checks if the sorting is correct
                    while (true)
                    {
                        Console.WriteLine("Sortering (S)tigande eller (F)allande?");
                        userChoiceAcending = Console.ReadLine().ToLower();

                        if (userChoiceAcending == "s" || userChoiceAcending == "f")
                        {
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Ogiltigt val. Vänligen välj 'S' eller 'F'.");
                        }
                        Console.ReadKey();
                        Console.Clear();
                    }

                    if (userChoiceAcending == "s")
                    {
                        ascending = true;
                    }
                    else if (userChoiceAcending == "f")
                    {
                        ascending = false;
                    }
                    
                    ok = true;
                    Console.Clear();
                }
                
                // checks the order of the list
                var students = context.Students.ToList();

                if (sortBy == "n" && ascending == true)
                {
                    students = students.OrderBy(s => s.FirstName).ToList(); // Namn, Ascending
                }
                else if (sortBy == "n" && ascending == false)
                {
                    students = students.OrderByDescending(s => s.FirstName).ToList(); // Namn, Descending
                }
                else if (sortBy == "e" && ascending == true)
                {
                    students = students.OrderBy(s => s.LastName).ToList(); // Efternamn, Ascending
                }
                else if (sortBy == "e" && ascending == false)
                {
                    students = students.OrderByDescending(s => s.LastName).ToList(); // Efternamn, Descending
                }

                // Displays student in the order that was chosen.
                Console.WriteLine("Samtliga Elever:\n");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FirstName} {student.LastName}");
                }
                Console.WriteLine("\nTryck på valfri tangent för återgå till menyn...");
                Console.ReadKey();
            }
        }

        public void GetAllStudentsInAClass()
        {
            using (var context = new SchoolDbContext())
            {
                Console.WriteLine("Här är en lista över alla klasser:");
                // Hämta alla klasser och skriv ut deras namn
                var classes = context.Classes.ToList();
                foreach (var item in classes)
                {
                    Console.WriteLine($"* {item.Name}");
                }
                
                Console.WriteLine("\nVilken klass vill du kolla på?");
                Console.Write("Svar: ");
                var userChoice = Console.ReadLine().ToUpper();
                
                // Hämtar den valda klassen med dess elever
                var activeClass = context.Students
                    .Include(c => c.Class)
                    .Where(c => c.Class.Name == userChoice).ToList();

                Console.Clear();

                if (activeClass.Count != 0)
                {
                    Console.WriteLine($"Elever i {userChoice}:\n");

                    foreach (var student in activeClass)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("Ingen klass hittad");
                }
                Console.WriteLine("\nTryck på valfri tangent för återgå till menyn...");
                Console.ReadKey();
            }
        }

        public void AddStaff()
        {
            using (var context = new SchoolDbContext())
            {
                Console.WriteLine("Vänligen ange informationen nedan\n");
                Console.Write("Förnamn: ");
                var firstName = Console.ReadLine()?.Trim();

                Console.Write("Efternamn: ");
                var lastName = Console.ReadLine()?.Trim();

                var roles = context.Roles.ToList();

                if (!roles.Any())
                {
                    Console.WriteLine("Det finns inga roller tillgängliga. Lägg till en roll först.");
                    return;
                }

                Console.WriteLine("\nTillgängliga roller:");
                foreach (var role in roles)
                {
                    Console.WriteLine($"{role.RoleId} - {role.RoleName}");
                }

                int staffRole;
                while (true)
                {
                    Console.Write("\nVälj det ID du vill lägga till: ");
                    if (int.TryParse(Console.ReadLine(), out staffRole) && roles.Any(roles => roles.RoleId == staffRole))
                    {
                        break;
                    }
                    Console.WriteLine("Ogiltigt roll ID. Försök igen.");
                }

                try
                {
                    var newStaff = new Staff
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        RoleId = staffRole
                    };

                    context.Staff.Add(newStaff);
                    context.SaveChanges();
                    Console.WriteLine("Personal tillagd.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ett fel inträffade vid tilldelning av personal: {ex.Message}");
                }
            }
            Console.WriteLine("\nTryck på valfri tangent för återgå till menyn...");
            Console.ReadKey();
        }
    }
}