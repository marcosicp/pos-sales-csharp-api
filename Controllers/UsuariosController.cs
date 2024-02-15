using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository noteRepository)
        {
            _usuariosRepository = noteRepository;
        }

        [HttpPost("login")]
        public async Task<UsuarioModel> Login()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var saras = await reader.ReadToEndAsync();
                    var userRequest = JsonConvert.DeserializeObject<LoginRequest>(saras);
                    //var salt = "$2b$12$7i/hXahqw7.Pu0rwk2YTYO";

                    var usuario = _usuariosRepository.GetUsuario(userRequest.Email);
                    usuario.Wait();

                    if (usuario.Result != null)
                    {
                        return BCrypt.BCryptHelper.CheckPassword(userRequest.Pass, usuario.Result.Password) ? usuario.Result : new UsuarioModel();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                

                return null;
            }
        }

        [HttpDelete("DeleteUsuario/{id}")]
        public void DeleteProveedor(string id)
        {
            _usuariosRepository.DeleteUsuario(id);
        }

        [HttpGet("GetAllUsuarios")]
        public async Task<IEnumerable<UsuarioModel>> GetAllUsuarios()
        {
            return await _usuariosRepository.GetAllUsuarios();
        }

        [HttpPost("AddUsuario")]
        public async Task<IEnumerable<UsuarioModel>> AddUsuario()
        {
            var usuarioList = new List<UsuarioModel>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(request);
                usuario.Creado = DateTime.Now;
                string salt = BCrypt.BCryptHelper.GenerateSalt(6);
                var hashedPassword = BCrypt.BCryptHelper.HashPassword(usuario.Password, salt);
                usuario.Password = hashedPassword;

                await _usuariosRepository.AddUsuario(usuario);

                var reqOk = usuario.Id != null;

                usuarioList.Add(usuario);
            }
            return usuarioList;
        }

        [HttpPost("CambiarPass")]
        public async Task<IEnumerable<UsuarioModel>> CambiarPass()
        {
            var usuarioModel = new List<UsuarioModel>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                //var cambiosPass = JsonConvert.DeserializeObject<CambiarPasswordModel>(request);
                var cambiosPass = new CambiarPasswordModel();//JsonConvert.DeserializeObject<CambiarPasswordModel>(request);
                cambiosPass.Usuario = await _usuariosRepository.GetUsuario("");
                cambiosPass.Creado = DateTime.Now;
                cambiosPass.Pass = "";
                cambiosPass.Usuario.Editado = DateTime.Now;
                string salt = BCrypt.BCryptHelper.GenerateSalt(6);
                var hashedPassword = BCrypt.BCryptHelper.HashPassword(cambiosPass.Pass, salt);
                cambiosPass.Usuario.Password = hashedPassword;

                await _usuariosRepository.AddUsuario(cambiosPass.Usuario);

                usuarioModel.Add(cambiosPass.Usuario);
                await _usuariosRepository.UpdateUsuario(cambiosPass.Usuario);

                return usuarioModel;
            }
        }

        [HttpPost("UpdateUsuario")]
        public async Task<UsuarioModel> UpdateUsuario()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(request);
                usuario.Editado = DateTime.Now;

                await _usuariosRepository.AddUsuario(usuario);

                return await _usuariosRepository.UpdateUsuario(usuario);
            }
        }

    }
}
