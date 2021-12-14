using System;
using System.Collections;
using System.Collections.Generic;
using API.Context;
using API.EmployeeRepository;
using API.Model;
using API.Models;
using API.ViewModel;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {

        private readonly MyContext context;
       

        public AccountRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public string Login(LoginVM loginVM)
        {
            var result = "0";
            var cekPassword = "";
            try
            {
                var cekEmail = context.Employees.Where(e => e.Email == loginVM.Email).FirstOrDefault();
                var cekPhone = context.Employees.Where(e => e.Phone == loginVM.Phone).FirstOrDefault();
                var NIK = (from e in context.Set<Employee>()
                           where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                           select e.NIK).Single();

                cekPassword = (from e in context.Set<Employee>()
                               join a in context.Set<Account>() on e.NIK equals a.NIK
                               where e.Email == loginVM.Email || e.Phone == loginVM.Phone
                               select a.Password).Single();

                if (cekEmail != null || cekPhone != null)
                {
                    var tryPass = Hashing.Hashing.ValidatePassword(loginVM.Password, cekPassword);
                    if (tryPass != false)
                    {
                        result = NIK;
                    }
                    else
                    {
                        result = "2";
                    }
                }
                else
                {
                    result = "3";
                }
            }
            catch (Exception)
            {
                result = "0";
            }
            return result;

           
        }

        public IEnumerable<GetProfileVM> GetProfile(string Key)
        {
            var cekNik = context.Employees.Find(Key);

            if (cekNik != null)
            {
                var getProfile = (from e in context.Employees
                                  where e.NIK == Key
                                  join a in context.Accounts on e.NIK equals a.NIK
                                  join p in context.Profiling on a.NIK equals p.NIK
                                  join ed in context.Education on p.EducationId equals ed.Id
                                  join u in context.University on ed.UniversityId equals u.Id
                                  select new GetProfileVM
                                  {
                                      NIK = e.NIK,
                                      FirstName = e.FirstName,
                                      LastName = e.LastName,
                                      Phone = e.Phone,
                                      BirthDate = e.BirthDate,
                                      Salary = e.Salary,
                                      Email = e.Email,
                                      Id = p.EducationId,
                                      Degree = ed.Degree,
                                      Gpa = ed.Gpa,
                                      UniversityId = ed.UniversityId,
                                      Name = u.Name
                                  });

                return getProfile;
            }

            return null;

        }


        public int ChangePassword(ChangePass changePass)
        {
            var checkEmail = context.Employees.Where(b => b.Email == changePass.Email).FirstOrDefault();
            if (checkEmail != null)
            {
                if (changePass.NewPassword == changePass.ConfirmPassword)
                {
                    var nik = (from e in context.Set<Employee>()
                               where e.Email == changePass.Email
                               join a in context.Set<Account>() on e.NIK equals a.NIK
                               select e.NIK).Single();
                    var password = (from e in context.Set<Employee>()
                                    where e.Email == changePass.Email
                                    join a in context.Set<Account>() on e.NIK equals a.NIK
                                    select a.Password).Single();
                    var checkPassword = Hashing.Hashing.ValidatePassword(changePass.OldPassword, password);
                    if (checkPassword == false)
                    {
                        return 4;
                    }
                    var original = context.Accounts.Find(nik);
                    if (original != null)
                    {
                        original.Password = Hashing.Hashing.HashPassword(changePass.NewPassword);
                        context.SaveChanges();
                        return 1;
                    }
                }
                else
                {
                    return 3;
                }
            }
            return 2;
        }

        public int ForgotPassword(ForgotVM forgotVM)
        {          
            var checkEmail = context.Employees.Where(b => b.Email == forgotVM.Email).FirstOrDefault();
            if(checkEmail != null)
            {
                var FirstName = (from e in context.Set<Employee>()
                                 where e.Email == forgotVM.Email
                                 select e.FirstName).Single();

                var LastName = (from e in context.Set<Employee>()
                                where e.Email == forgotVM.Email
                                select e.LastName).Single();

                var name = FirstName + " " + LastName;

                string passwordnew = Guid.NewGuid().ToString().Substring(0, 12);
                var NIK = (from e in context.Set<Employee>()
                           where e.Email == forgotVM.Email
                           join a in context.Set<Account>() on e.NIK equals a.NIK
                           select e.NIK).Single();
          

                var original = context.Accounts.Find(NIK);
                DateTimeOffset now = (DateTimeOffset)DateTime.Now;

                if (original != null)
                {
                    original.Password = Hashing.Hashing.HashPassword(passwordnew);
                    context.SaveChanges();

                    MailMessage mail = new MailMessage();
                    mail.To.Add(forgotVM.Email);
                    mail.From = new MailAddress("vainngod@gmail.com", "Forgot Password!!", System.Text.Encoding.UTF8);
                    mail.Subject = "RESET PASSWORD " + now;
                    mail.SubjectEncoding = System.Text.Encoding.UTF8;

                    mail.Body =
                                "Hi " + name + " <br /> New Password is " + passwordnew;
                    
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.High;
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("vainngod@gmail.com", "Vevevovivo123");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    try
                    {
                        client.Send(mail);
                        return 1;
                    }
                    catch(Exception)
                    {
                        return 3;
                    }
                }
            }
            return 2;
        }

    }
}

   

    