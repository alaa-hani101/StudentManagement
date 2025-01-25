using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
namespace StudentManagement
{
    public  class StudentRepository : IStudentRepository
    {
       
        private string connectionString = "constr";
        Student student;
       
        public StudentRepository(IConfiguration configuration)
        {
            // Get the connection string from the configuration
            connectionString = configuration.GetSection(connectionString).Value;
        }
        public void StudentsDetails()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            var sqlCommand = "SELECT * FROM STUDENTS";

            SqlCommand command = new SqlCommand(sqlCommand, connection);

            command.CommandType = CommandType.Text;

            connection.Open();

            SqlDataReader reader = command.ExecuteReader();
            Student st;

            while (reader.Read())
            {
                st = new Student
                {
                    StudentID = Convert.ToInt32(reader["StudentID"]),
                    FirstName = Convert.ToString(reader["FirstName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                    Grade = Convert.ToDecimal(reader["Grade"]),
                };
                Console.WriteLine(st);
            }
            connection.Close();
        }

        public void  AddStudent( )
        {
            Console.WriteLine("Enter student details:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Date of Birth (yyyy-MM-dd): ");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            Console.Write("Grede: ");
            decimal grade = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("InsertStudent", connection);

                command.Parameters.AddWithValue("@FirstName", firstName);
                command.Parameters.AddWithValue("@LastName", lastName);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                command.Parameters.AddWithValue("@Grade", grade);

                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                if (command.ExecuteNonQuery() > 0)
                {
                    Console.WriteLine($"Student added successfully");
                }
                else
                {
                    Console.WriteLine($"Student was not added");
                }

                connection.Close();

            }
        }

        public void  FilterStudentsByGrade( )
        {
            Console.WriteLine("Enter the grade you need to filter throgh: ");
            decimal grade = decimal.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("GetStudentsByGrade", connection);

                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@grade", grade);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                Student st;

                while (reader.Read())
                {
                    st = new Student
                    {
                        StudentID = Convert.ToInt32(reader["StudentID"]),
                        FirstName = Convert.ToString(reader["FirstName"]),
                        LastName = Convert.ToString(reader["LastName"]),
                        DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                        Grade = Convert.ToDecimal(reader["Grade"]),
                    };
                    Console.WriteLine(st);
                }
                connection.Close();

            }
        }

        public int CalculateStudentAge()
        {
            Console.WriteLine(" Enter the Date of Birth to calculate the Age");
            DateTime dateOfBirth = DateTime.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT dbo.CalculateAge(@DateOfBirth)", connection);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);

                connection.Open();
                return (int)command.ExecuteScalar();
            }
        }

        public override string ToString()
        {
            return $"Student ID: {student.StudentID}" +
                $", Name: {student.FirstName} {student.LastName}, " +
                $"DOB: {student.DateOfBirth.ToShortDateString()}, " +
                $"Grade: {student.Grade}";
        }

    }
}

