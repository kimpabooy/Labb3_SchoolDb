using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Labb3_SchoolDb
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DatabaseManager dbM = new DatabaseManager();
            //dbM.GetAllStudents(); 
            dbM.GetAllStudentsInAClass();
            Console.ReadKey();
        }
    }
}