using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModel
{
    public class EmployeeVM
    {

        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Genders Genders { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public string Name { get; set; }
    }

    public enum Genders
    {
        Male,
        Female
    }
}

