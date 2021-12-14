using System;
using System.Net;
using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RoleController : BasesController<Role, RoleRepository, int>
    {
        public RoleController(RoleRepository repository) : base(repository)
        {
        }
    }
}
