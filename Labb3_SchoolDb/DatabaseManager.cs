using Labb3_SchoolDb.Data;
using Labb3_SchoolDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_SchoolDb
{
    public class DatabaseManager
    {

        public void Read()
        {

        }

        public void GetAllStudents()
        {
            using (var context = new SchoolDbContext())
            {
                var students = context.Students;
                foreach  (var student in context.Students)
                {
                    Console.WriteLine($"{student.StudentId}. {student.FirstName} {student.LastName}");
                }
            }
        }

        public void GetAllStudentsInAClass()
        {
            using (var context = new SchoolDbContext())
            {
                // Hämta alla klasser och skriv ut deras namn
                var classes = context.Classes.ToList();
                foreach (var item in classes)
                {
                    Console.WriteLine($"{item.Name}");
                }

                Console.WriteLine("Vilken klass vill du kolla på?");
                Console.Write("Svar: ");
                var userChoice = Console.ReadLine().ToUpper();

                // Hämtar den valda klassen med dess elever
                var activeClass = context.Classes
                    .Include(c => c.Students)
                    .FirstOrDefault(c => c.Name.ToUpper() == userChoice);

                if (activeClass != null)
                {
                    Console.WriteLine($"Elever i {activeClass.Name}:");
                    foreach (var student in activeClass.Students)
                    {
                        Console.WriteLine($"{student.FirstName} {student.LastName}");
                    }
                }
                else
                {
                    Console.WriteLine("Ingen klass hittad");
                }
            }
        }
       
        public void AddStaff()
        {
            using(var context = new SchoolDbContext())
            {
                var showClass = context.Classes;

                foreach (var item in context.Classes)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }
        }





    }
}
