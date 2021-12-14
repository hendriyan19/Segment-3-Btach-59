using System;
using Newtonsoft.Json;

namespace API.ViewModel
{
    public class RegisterVM
    {

        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public string Password { get; set; }
        public string Degree { get; set; }
        public string Gpa { get; set; }
        public int UniversityId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

}

