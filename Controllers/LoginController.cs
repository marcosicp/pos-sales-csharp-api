using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        private IConfiguration _config;

        public LoginController(IConfiguration config, IUsuariosRepository noteRepository)
        {
            _config = config;
            _usuariosRepository = noteRepository;
        }

        [HttpPost("Login")]
        public async Task<UsuarioModel> Login()
        {
            //IActionResult response = Unauthorized();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var saras = await reader.ReadToEndAsync();
                var userRequest = JsonConvert.DeserializeObject<LoginRequest>(saras);
                var usuario = await _usuariosRepository.GetUsuario(userRequest.Email);
                //usuario.Wait();
                if (usuario!= null)
                {
                    var usuarioOK = BCrypt.BCryptHelper.CheckPassword(userRequest.Pass, usuario.Password) ? usuario: null;
                    if (usuarioOK != null)
                    {
                        usuarioOK.Token = GenerateJSONWebToken(usuarioOK);
                        
                        // NO SE ENVIA LA PASS AL FRONT END
                        usuarioOK.Password = "";
                        return usuarioOK;
                    }
                }

                return null;
            }
            
        }

        private string GenerateJSONWebToken(UsuarioModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, userInfo.Usuario),
                 new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                 new Claim("DateOfJoing", userInfo.Creado.ToString()),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                 };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Issuer"],
            claims,
            expires: DateTime.Now.AddMinutes(120),
            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}