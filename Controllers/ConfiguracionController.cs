using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DieteticaPuchiApi.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class ConfiguracionController : ControllerBase
    {
        private readonly IConfiguracionRepository _configRepository;

        public ConfiguracionController(IConfiguracionRepository configRepository)
        {
            _configRepository = configRepository;
        }

        [HttpGet("GetAllConfiguracion")]
        public async Task<ConfiguracionModel> GetAllConfiguracion()
        {
            return await _configRepository.GetAllConfiguracion();
        }


        [HttpPost("UpdateConfiguracion")]
        public async Task<bool> UpdateConfiguracion()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var config = JsonConvert.DeserializeObject<ConfiguracionModel>(request);
                config.Editado = DateTime.Now;

                return await _configRepository.UpdateConfiguracion(config);
            }
        }

    }
}
