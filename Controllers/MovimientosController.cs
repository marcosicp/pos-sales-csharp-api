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
    public class MovimientosController : Controller
    {
        private readonly IMovimientosRepository _movimientosRepository;

        public MovimientosController(IMovimientosRepository movimientosRepository)
        {
            _movimientosRepository = movimientosRepository;
        }

        [HttpGet("GetAllMovimientos")]
        public async Task<IEnumerable<MovimientoModel>> GetAllMovimientos()
        {
            return await _movimientosRepository.GetAllMovimientos();
        }

        [HttpGet("EstadoCaja")]
        public IEnumerable<bool> EstadoCaja()
        {
            return _movimientosRepository.EstadoCaja();
        }

        [HttpGet("IngresosCaja")]
        public double? IngresosCaja()
        {
            return _movimientosRepository.IngresosCaja();
        }

        [HttpGet("AperturaInicialCaja")]
        public IEnumerable<double?> AperturaInicialCaja()
        {
            var montoApertura = new List<double?> {_movimientosRepository.AperturaInicialCaja()};
            return montoApertura;
        }

        [HttpGet("EgresosCaja")]
        public double? EgresosCaja()
        {
            return _movimientosRepository.EgresosCaja();
        }

        [HttpGet("VentasDelDia")]
        public KeyValuePair<int, double>? VentasDelDia()
        {
            return _movimientosRepository.VentasDelDia();
        }

        [HttpPost("AbrirCaja")]
        public async Task<List<bool>> AbrirCaja()
        {
            var ok = new List<bool>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var mov = await reader.ReadToEndAsync();
                var movRequest = JsonConvert.DeserializeObject<MovimientoModel>(mov);
                movRequest.Creado = DateTime.Now;
                //movRequest.Usuario = JsonConvert.DeserializeObject<UsuarioModel>(movRequest.Usuario);
                var abrioCaja = _movimientosRepository.AbrirCaja(movRequest);

                var reqOk = abrioCaja;

                ok.Add(reqOk);
            }

            return ok;
        }

        [HttpPost("CerrarCaja")]
        public async Task<List<bool>> CerrarCaja()
        {
            var ok = new List<bool>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var mov = await reader.ReadToEndAsync();
                var movRequest = JsonConvert.DeserializeObject<MovimientoModel>(mov);
                movRequest.Creado = DateTime.Now;
                var abrioCaja = _movimientosRepository.CerrarCaja(movRequest);

                var reqOk = abrioCaja;

                ok.Add(reqOk);
            }

            return ok;
        }

        [HttpGet("CierreCajaCalculo")]
        public async Task<MovimientoModel> CierreCajaCalculo()
        {
            return await _movimientosRepository.GetCierreCajaCalculo();
        }

        [HttpPost("AddMovimiento")]
        public async Task<List<bool>> AddMovimiento()
        {
            var ok = new List<bool>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var mov = await reader.ReadToEndAsync();
                    var movRequest = JsonConvert.DeserializeObject<MovimientoModel>(mov);
                    movRequest.Creado = DateTime.Now;
                    movRequest.FechaMovimiento = DateTime.Now;

                    if (movRequest.Tipo == "APERTURA")
                    {
                        _movimientosRepository.AbrirCaja(movRequest);
                    }
                    else if (movRequest.Tipo == "CIERRE")
                    {
                        _movimientosRepository. CerrarCaja(movRequest);
                    } 
                    else {
                        _movimientosRepository.AddMovimiento(movRequest);
                    }

                    ok.Add(true);
                }
                catch (Exception ex)
                {
                    throw ex;
                    ok.Add(false);
                }
                
            }

            return ok;
        }
    }
}
