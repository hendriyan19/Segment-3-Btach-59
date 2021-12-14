using System;
using Newtonsoft.Json;

namespace API.ViewModel
{
    public class LoginVM
    {
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore]

        public string Degree { get; set; }
        public string Gpa { get; set; }
        public int UniversityId { get; set; }
    }
}
