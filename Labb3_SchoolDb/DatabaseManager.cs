using Labb3_SchoolDb.Data;
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

        public void AddStaff()
        {

        }





    }
}
