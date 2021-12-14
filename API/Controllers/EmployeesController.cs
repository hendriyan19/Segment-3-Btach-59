using System;
using System.Collections.Generic;
using System.Net;
using API.Base;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee, Repository.Data.EmployeeRepository, string>
    {
        
        
        private readonly Repository.Data.EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        public EmployeesController(Repository.Data.EmployeeRepository repository, IConfiguration configuration) : base(repository)
        {
            this.employeeRepository = repository;
            this._configuration = configuration;
        }

        [HttpPost("Register")]
        public ActionResult Post(RegisterVM registerVM)
        {
            
            var result = employeeRepository.Register(registerVM);
            try
            {

            return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di create" });
               
            } catch(Exception)

            {
                return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Gagal Masuk Data" });
            }
        }

        //[Authorize(Roles = "Director,Manager")]
        [HttpGet("Get")]
        public ActionResult GetAll()
        {

            var result = employeeRepository.EmployeeAllData();
            return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil di tembak" });
        }

        [HttpGet("getregister")]
        public ActionResult GetRegister()
        {
            //var result = "Get All Register Employee data berhasil di ambil";
            var result = employeeRepository.EmployeeAllData();
            return Ok(result);
        }


        [HttpGet("Profile/{NIK}")]
        public ActionResult<RegisterVM> Profile(string NIK)
        {
            var result = employeeRepository.GetProfile(NIK);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data ditemukan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {NIK} tidak ditemukan." });
        }

        [HttpGet("TestCORS")]
        public ActionResult TestCORS()
        {
            var result = employeeRepository.getAllEm();
            return Ok(new { status = HttpStatusCode.OK, result, Message = "Test Cors Berhasil Data berhasil di tembak" });

        }
    }
}
