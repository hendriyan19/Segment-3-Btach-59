﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using API.Model;
using Newtonsoft.Json;

namespace API.Models
{
    [Table("tb_employee")]
    public class Employee
    {   
        [Key]
        public string NIK { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public int Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        [JsonIgnore]
        public virtual Account Account { get; set; }  
    }

    public enum Gender
    {
        Male,
        Female
    }

}
