using API.Base;
using API.Context;
using API.Model;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : BasesController<Account, AccountRepository, string>
    {

        private AccountRepository accountRepository;
        private readonly MyContext context;
        public IConfiguration _configuration; 
        public AccountsController(AccountRepository repository, MyContext myContext, IConfiguration configuration) : base(repository)
        {
            this.accountRepository = repository;
            this.context = myContext;
            this._configuration = configuration; 

        }

     

        [HttpPost("Login")]
        public ActionResult Post(LoginVM loginVM)
        {

            var result = accountRepository.Login(loginVM);
            if (result == "2")
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Incorrect Password" });
            }
            else if (result == "3")
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Login Failed" });
            }
            else if (result == "0")
            {
                return NotFound(new { status = HttpStatusCode.NoContent, result, messageResult = "Incorrect Email or Phone " });
            }
            else
            {

                var profile = accountRepository.GetProfile(result);

                if (profile != null)
                {
                    var getUserData =  (from e in context.Employees
                                       where e.Email == loginVM.Email
                                       join a in context.Set<Account>() on e.NIK equals a.NIK
                                       join ar in context.Set<AccountRole>() on e.NIK equals ar.AccountNIK
                                       join r in context.Set<Role>() on ar.RoleId equals r.RoleId
                                       select new {
                                           name = r.Name,
                                           email = e.Email
                                             }).ToList();
                    
                    
                    var claims = new List<Claim>
                    {
                        new Claim("Email", getUserData[0].email),                      
                    };
                    foreach (var useRole in getUserData)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, useRole.name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );

                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("TokenSecurity", idtoken.ToString()));
                    //return Ok(new { status = HttpStatusCode.OK, Token=idtoken, Message = "Login Succes" });    
                    return Ok(new JWTokenVM { Token = idtoken, Messages = "Login Success" });
                }
                else
                {
                    return BadRequest(new { Status = HttpStatusCode.BadRequest, Message = "Data Not Found" });
                }

            }
        }

        //[HttpPost("Auth/")]

        //public async Task<IActionResult> Auth(LoginVM login)
        //{
        //    if (!(await accountRepository.Login(loginVM)))
        //    {

        //        return Ok(new JWTokenVM { Token = null, Message = "Login Failed" });
        //    }

        //    var userData = await accountRepository.GetClaim(login);
        //    var token = jwtHandler.GenerateToken(userData);
        //    return Ok(new JWTokenVM { Token = token, Messages = "Login Sucessful" });

        //}

        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }

        [HttpPost("ChangePass")]
        public ActionResult ChangePassword(ChangePass changePass)
        {
            var result = accountRepository.ChangePassword(changePass);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Konfirmasi password tidak sesuai dengan new password" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, message = "Data Password kamu telah diperbarui" });
            }
            else if (result == 4)
            {
                return Ok(new { status = HttpStatusCode.NotFound, result = result, message = "Password lama tidak sesuai dengan database" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }

        [HttpPost("ForgotPassword")]
        public ActionResult ForgotPassword(ForgotVM forgotVM)
        {
            var result = accountRepository.ForgotPassword(forgotVM);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Password berhasil diubah, tetapi email tidak terkirim" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "periksa email kamu" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }
    }
}



    

