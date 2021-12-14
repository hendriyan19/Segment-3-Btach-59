using System;
namespace API.ViewModel
{
    public class ChangePass
    {
 
        public string Email { get; set; }
  
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
