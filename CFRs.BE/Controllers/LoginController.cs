using BankReconcile.BLL.BANK_RECONCILE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CFRs.BE.Model;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CFRs.BE.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (string.IsNullOrEmpty(loginDTO.UserName) || string.IsNullOrEmpty(loginDTO.Password))
                    return BadRequest("Username and / or Password not specified");

                DataTable dtUserAPI = Token_BLL.Instance.CheckLoginBLL(loginDTO.UserName, loginDTO.Password);
                if (dtUserAPI.Rows.Count == 0)
                {
                    return Unauthorized("Invalid Username or Password !!!");
                }
                else
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("IntersoftSolution"));

                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                    var jwtSecurityToken = new JwtSecurityToken(
                        issuer: "Sakchai",
                        audience: "http://localhost:57942/login",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );

                    return Ok(new JwtSecurityTokenHandler().
                    WriteToken(jwtSecurityToken));
                }
            }
            catch
            {
                return BadRequest("An error occurred in generating the token");
            }
        }
    }
}
