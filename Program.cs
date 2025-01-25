using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace StudentManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appsettings.json")
                   .Build();


            StudentRepository repository = new StudentRepository(configuration);

            Console.WriteLine("Welcome to Student Management System!");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Add a new student");
            Console.WriteLine("2. View all students");
            Console.WriteLine("3. View all students who have grade greater than specified grade .");
            Console.WriteLine("4. Calculate the Age for specified Date of Birth  .");

            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice (1-3): ");

            // Read the user's choice
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    repository.AddStudent();
                    break;
                case "2":
                    repository.StudentsDetails();
                    break;
                case "3":
                    repository.FilterStudentsByGrade();
                    return;
                case "4":
                    Console.WriteLine( repository.CalculateStudentAge());
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }





            
        }
    }
}
