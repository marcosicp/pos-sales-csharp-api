using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ClientesController : Controller
    {
        private readonly IClientesRepository _clienteRepository;

        public ClientesController(IClientesRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpPost("AddCliente")]
        public async Task<IEnumerable<bool>> AddCliente()
        {
            var ok = new List<bool>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteModel>(request);

                await _clienteRepository.AddCliente(cliente);
                var reqOk = cliente.Id!=null;

                ok.Add(reqOk);
            }
            return ok;
        }

        [HttpDelete("DeleteCliente/{id}")]
        public void DeleteCliente(string id)
        {
            _clienteRepository.DeleteCliente(id);
        }

        [HttpGet("GetAllClientes")]
        public async Task<IEnumerable<ClienteModel>> GetAllClientes()
        {
            return await _clienteRepository.GetAllClientes();
        }

        [HttpPost("UpdateCliente")]
        public async Task<ClienteModel> UpdateCliente()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var cliente = JsonConvert.DeserializeObject<ClienteModel>(request);
                cliente.Editado = DateTime.Now;

                await _clienteRepository.AddCliente(cliente);

                return await _clienteRepository.UpdateCliente(cliente);
            }
        }
    }
}
