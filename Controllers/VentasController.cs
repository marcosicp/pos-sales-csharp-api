using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VentasController : Controller
    {
        private readonly IVentasRepository _ventasRepository;
        private readonly IProductosRepository _productosRepository;
        private readonly IClientesRepository _clientesRepository;
        private readonly IUsuariosRepository _usuariosRepository;

        public VentasController(IVentasRepository ventasRepository, IProductosRepository productosRepository, IClientesRepository clientesRepository, IUsuariosRepository usuariosRepository)
        {
            _ventasRepository = ventasRepository;
            _productosRepository = productosRepository;
            _clientesRepository = clientesRepository;
            _usuariosRepository = usuariosRepository;
        }

        [HttpGet("GetAllVentas")]
        public async Task<IEnumerable<VentaModel>> GetAllVentas()
        {
            return await _ventasRepository.GetAllVentas();
        }

        [HttpGet("GetTotalVentasHoy")]
        public async Task<double?> GetTotalVentasHoy()
        {
            return await _ventasRepository.GetTotalVentasHoy();
        }

        [HttpGet("GetTotalVentasMes")]
        public async Task<double?> GetTotalVentasMes()
        {
            return await _ventasRepository.GetTotalVentasMes();
        }

        [HttpGet("GetTotalVentasEfectivoMes")]
        public async Task<double?> GetTotalVentasEfectivoMes()
        {
            return await _ventasRepository.GetTotalVentasEfectivoMes();
        }

        [HttpGet("GetTotalVentasDebitoHoy")]
        public async Task<double?> GetTotalVentasDebitoHoy()
        {
            return await _ventasRepository.GetTotalVentasDebitoHoy();
        }

        [HttpGet("GetTotalVentasDebitoMes")]
        public async Task<double?> GetTotalVentasDebitoMes()
        {
            return await _ventasRepository.GetTotalVentasDebitoMes();
        }

        [HttpGet("GetTotalVentasUnaCuotaMes")]
        public async Task<double?> GetVentasUnaCuota()
        {
            return await _ventasRepository.GetTotalVentasUnaCuotaMes();
        }

        [HttpGet("GetTotalVentasTresCuotasMes")]
        public async Task<double?> GetTotalVentasTresCuotasMes()
        {
            return await _ventasRepository.GetTotalVentasTresCuotasMes();
        }

        [HttpGet("GetTotalVentasUnaCuotaHoy")]
        public async Task<double?> GetTotalVentasUnaCuotaHoy()
        {
            return await _ventasRepository.GetTotalVentasUnaCuotaHoy();
        }

        [HttpGet("GetTotalVentasTresCuotasHoy")]
        public async Task<double?> GetTotalVentasTresCuotasHoy()
        {
            return await _ventasRepository.GetTotalVentasTresCuotasHoy();
        }

        [HttpGet("GetTotalVentasMercadoPagoMes")]
        public async Task<double?> GetVentasMercadoPago()
        {
            return await _ventasRepository.GetTotalVentasMercadoPagoMes();
        }

        [HttpGet("GetTotalVentasTransferenciaMes")]
        public async Task<double?> GetVentasTransferencia()
        {
            return await _ventasRepository.GetTotalVentasTransferenciaMes();
        }


        [HttpGet("GetTotalVentasPedidosYaMes")]
        public async Task<double?> GetTotalVentasPedidosYaMes()
        {
            return await _ventasRepository.GetTotalVentasPedidosYaMes();
        }


        #region VentasDeHoy

        [HttpGet("GetTotalVentasPedidosYaHoy")]
        public async Task<double?> GetTotalVentasPedidosYaHoy()
        {
            return await _ventasRepository.GetTotalVentasPedidosYaHoy();
        }

        [HttpGet("GetTotalVentasEfectivoHoy")]
        public async Task<double?> GetTotalVentasEfectivoHoy()
        {
            return await _ventasRepository.GetTotalVentasEfectivoHoy();
        }


        [HttpGet("GetTotalVentasMercadoPagoHoy")]
        public async Task<double?> GetTotalVentasMercadoPagoHoy()
        {
            return await _ventasRepository.GetTotalVentasMercadoPagoHoy();
        }

        [HttpGet("GetTotalVentasTransferenciaHoy")]
        public async Task<double?> GetTotalVentasTransferenciaHoy()
        {
            return await _ventasRepository.GetTotalVentasTransferenciaHoy();
        }

        #endregion VentasDeHoy

        [HttpPost("AddVenta")]
        public async Task<string> AddVenta()
        {
            var venta = new VentaModel();

            try
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var request = await reader.ReadToEndAsync();
                    venta = JsonConvert.DeserializeObject<VentaModel>(request);
                    venta.Editado = null;
                    venta.Pendiente = false;
                    if (venta.TipoTransaccion=="PEDIDOS YA") {
                        venta.Pendiente = true;
                    }
                    
                    venta.Creado = DateTime.Now;

                    venta = await _ventasRepository.AddVenta(venta);
                    await _productosRepository.ReducirStock(venta.ProductosVenta);
                }
            }
            catch (Exception ex)
            {
                return "ERROR";
            }

            return "OK";
        }

        [HttpPost("UpdateVenta")]
        public async Task<VentaModel> UpdateVenta()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var venta = JsonConvert.DeserializeObject<VentaModel>(request);
                venta.Editado = DateTime.Now;

                await _ventasRepository.AddVenta(venta);

                return await _ventasRepository.UpdateVenta(venta);
            }
        }

        [HttpPost("NotificarPago")]
        public async Task<String> NotificarPago()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var venta = JsonConvert.DeserializeObject<VentaModel>(request);
                

                return await _ventasRepository.NotificarPago(venta);
            }
        }
    }
}