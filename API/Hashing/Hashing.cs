using System;
using System.Collections.Generic;
using System.Linq;
using API.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace API.Hashing
{
    public class Hashing
    {
        public static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }

       
    }
}
