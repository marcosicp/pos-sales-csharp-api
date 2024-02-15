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
    public class ProveedoresController : Controller
    {
        private readonly IProveedoresRepository _proveedorRepository;
        private readonly IComprasProveedoresRepository _compraProveedorRepository;

        public ProveedoresController(IProveedoresRepository proveedorRepository, IComprasProveedoresRepository comprasProveedorRepository)
        {
            _proveedorRepository = proveedorRepository;
            _compraProveedorRepository = comprasProveedorRepository;
        }

        [HttpPost("AddProveedor")]
        public async Task<IEnumerable<ProveedorModel>> AddProveedor()
        {
            var proveedorList = new List<ProveedorModel>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var proveedor = JsonConvert.DeserializeObject<ProveedorModel>(request);
                proveedor.Creado = DateTime.Now;

                await _proveedorRepository.AddProveedor(proveedor);

                var reqOk = proveedor.Id!=null;

                proveedorList.Add(proveedor);
            }
            return proveedorList;
        }

        [HttpDelete("DeleteProveedor/{id}")]
        public void DeleteProveedor(string id)
        {
            _proveedorRepository.DeleteProveedor(id);
        }

        [HttpGet("GetAllProveedores")]
        public async Task<IEnumerable<ProveedorModel>> GetAllProveedores()
        {
            return await _proveedorRepository.GetAllProveedores();
        }

        [HttpGet("GetAllComprasProveedores")]
        public async Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedores()
        {
            return await _proveedorRepository.GetAllComprasProveedores();
        }

        [HttpGet("GetAllComprasProveedoresCC")]
        public async Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedoresCC()
        {
            return await _proveedorRepository.GetAllComprasProveedoresCC();
        }

        [HttpPost("UpdateProveedor")]
        public async Task<ProveedorModel> UpdateProveedor()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var proveedor = JsonConvert.DeserializeObject<ProveedorModel>(request);
                proveedor.Editado = DateTime.Now;

                await _proveedorRepository.AddProveedor(proveedor);

                return await _proveedorRepository.UpdateProveedor(proveedor);
            }
        }

        [HttpPost("ConfirmarCompraProveedoreCC")]
        public async Task<CompraProveedorModel> ConfirmarCompraProveedoreCC()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();

                var compra= await _compraProveedorRepository.GetCompraProveedoresByID(request);
                compra.FechaLiquidacion= DateTime.Now;
                compra.Pendiente=false;

                
                
                
                return await _compraProveedorRepository.UpdateCompraProveedor(compra);
            }
        }
    }
}
