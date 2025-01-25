using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement
{
    public interface IStudentRepository
    {
        void AddStudent();
        void StudentsDetails();
        void FilterStudentsByGrade();
        int CalculateStudentAge();
    }
}
