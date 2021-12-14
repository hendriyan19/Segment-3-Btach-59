using System;
using System.Collections;
using System.Collections.Generic;
using API.Context;
using API.EmployeeRepository;
using API.Model;
using API.Models;
using API.ViewModel;
using System.Linq;
using API.Hashing;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext context;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public int Register(RegisterVM registerVM)
        {
            Console.WriteLine("Berhasil ambil repo Employee");
            Employee e = new Employee()
            {

                NIK = registerVM.NIK,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                Phone = registerVM.Phone,
                BirthDate = registerVM.BirthDate,
                Email = registerVM.Email,
                Salary = registerVM.Salary

            };

           
            context.Employees.Add(e);
            context.SaveChanges();
            Account a = new Account()
            {
                NIK = registerVM.NIK,
                Password = Hashing.Hashing.HashPassword(registerVM.Password)
            };
            context.Accounts.Add(a);
            context.SaveChanges();
            Education edu = new Education()
            {
                Gpa = registerVM.Gpa,
                Degree = registerVM.Degree,
                UniversityId = registerVM.UniversityId
            };
            context.Education.Add(edu);
            context.SaveChanges();


            Profiling profiling = new Profiling();
            {
                profiling.EducationId = edu.Id;
                profiling.NIK = registerVM.NIK;
            }
            context.Profiling.Add(profiling);
            context.SaveChanges();
            AccountRole ar = new AccountRole();
            {

                ar.AccountNIK = registerVM.NIK;
                ar.RoleId = 1;
            }
            context.AccountRole.Add(ar);
            context.SaveChanges();

            var result = context.SaveChanges();
            return result;

        }

        public IEnumerable getAllEm()
        {
            var query = from e in context.Set<Employee>()
                        select new

                        {
                            e.NIK,
                            e.FirstName,
                            e.LastName,
                            Gender = e.Gender == 0 ? "Male" : "Female",
                            e.Phone,
                            e.BirthDate,
                            e.Salary,
                            e.Email
                           
                        };
            return query.ToList();
        }
        public IEnumerable EmployeeAllData()
        {
            
            var query = from e in context.Set<Employee>()
                        join p in context.Set<Profiling>() on e.NIK equals p.NIK
                        join ed in context.Set<Education>() on p.EducationId equals ed.Id
                        join u in context.Set<University>() on ed.UniversityId equals u.Id

                        select new
                        
                        {
                            e.NIK,
                            e.FirstName,
                            e.LastName,
                            Gender = e.Gender == 0 ? "Male" : "Female",
                            e.Phone,
                            e.BirthDate,
                            e.Salary,
                            e.Email,
                            ed.Degree,
                            ed.Gpa,
                            u.Name
                        };
            return query.ToList();
        }
        public IEnumerable<RegisterVM> GetProfile(string Key)
        {

            var getProfile = (from e in context.Employees
                              where e.NIK == Key
                              join a in context.Accounts on e.NIK equals a.NIK
                              join p in context.Profiling on a.NIK equals p.NIK
                              join ed in context.Education on p.EducationId equals ed.Id
                              join u in context.University on ed.UniversityId equals u.Id

                              where Key == e.Email
                              select new RegisterVM
                              {
                                  NIK = e.NIK,
                                  FirstName = e.FirstName,
                                  LastName = e.LastName,
                                  Phone = e.Phone,
                                  BirthDate = e.BirthDate,
                                  Salary = e.Salary,
                                  Email = e.Email,
                                  Gender = (ViewModel.Gender)e.Gender,
                                  Id = p.EducationId,
                                  Degree = ed.Degree,
                                  Gpa = ed.Gpa,
                                  UniversityId = ed.UniversityId

                              });

            return getProfile;

        }


    }

    }

