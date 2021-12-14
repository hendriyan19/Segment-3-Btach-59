using System;
using System.Net;
using API.Base;
using API.Models;
using API.Repository.Data;
using API.Repository.Interface;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;



namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UniversityController : BasesController<University, UniversityRepository, int>
    {
        private Repository.Data.UniversityRepository universityRepository;

        public UniversityController(UniversityRepository repository) : base(repository)
        {
            this.universityRepository = repository;
        }

        [HttpGet("GetUniversity")]
        public ActionResult GetAll()
        {
            var result = universityRepository.GetIdUni();
            return Ok(new { status = HttpStatusCode.OK, result, Message = "Data berhasil diambil" });
        }
    }
}
