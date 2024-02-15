using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;

        public RolesController(IRolesRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        [HttpGet("GetAllRoles")]
        public async Task<IEnumerable<RolModel>> GetAllRoles()
        {
            return await _rolesRepository.GetAllRoles();
        }

        [HttpGet("GetRolById/{id}")]
        public async Task<RolModel> GetRolById(string id)
        {
            return await _rolesRepository.GetRolById(id);
        }

        [HttpPost("AddRol")]
        public async Task<RolModel> AddRol()
        {
            using (var loReader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var loSreader = await loReader.ReadToEndAsync();
                    var loRolesRequest = JsonConvert.DeserializeObject<RolModel>(loSreader);
                    var loRol = _rolesRepository.AddRol(loRolesRequest);
                    loRol.Wait();
                    return await loRol;
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR");
                    throw;
                }
            }
        }

        [HttpPost("UpdateRol")]
        public async Task<bool> UpdateRol()
        {
            using (var loReader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var loSreader = await loReader.ReadToEndAsync();
                    var loRolesRequest = JsonConvert.DeserializeObject<RolModel>(loSreader);
                    return await _rolesRepository.UpdateRol(loRolesRequest);
                }
                catch (Exception)
                {
                    Console.WriteLine("ERROR");
                    throw;
                }
            }
        }

        [HttpGet("DeleteRol/{id}")]
        public async Task<bool> DeleteRol(string id)
        {
            return await _rolesRepository.DeleteRol(id);
        }

        [HttpPost("DeleteAllRol")]
        public async Task<bool> DeleteAllRol()
        {
            return await _rolesRepository.DeleteAllRol();
        }
    }
}
