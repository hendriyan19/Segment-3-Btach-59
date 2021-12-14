using System;
using System.Collections.Generic;
using API.Context;
using API.EmployeeRepository;
using API.Models;
using API.ViewModel;
using System.Linq;
using System.Collections;

namespace API.Repository.Data
{
    public class UniversityRepository : GeneralRepository<MyContext, University, int>
    {
        private readonly MyContext context;
        public UniversityRepository(MyContext myContext) : base(myContext)
        {
            context = myContext;
        }

        public IEnumerable GetIdUni()
        {
            var GetUniv = (from p in context.Profiling
                            join ed in context.Education on p.EducationId equals ed.Id
                            join u in context.University on ed.UniversityId equals u.Id
                           group u by new { ed.UniversityId, u.Name } into v
                           select new
                            {
                            
                                val = v.Count(),
                                name= v.Key.Name
                              
                            });
            return GetUniv;
        }

    }
}