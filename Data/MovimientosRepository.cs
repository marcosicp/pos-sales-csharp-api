using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using System.Linq;

namespace DieteticaPuchiApi.Data
{
    public class MovimientosRepository : IMovimientosRepository
    {
        private readonly MongoDbContext _context;

        public MovimientosRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<IEnumerable<MovimientoModel>> GetAllMovimientos()
        {
            try
            {
                return await _context.Movimientos.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public bool AbrirCaja(MovimientoModel movimiento)
        {
            try
            {
                _context.Movimientos.InsertOne(movimiento);
                var estados = _context.EstadoCaja.Find( _=>true).FirstOrDefault();

                var filter = Builders<EstadoCaja>.Filter.Eq(s => s.Id, estados.Id);
                var update = Builders<EstadoCaja>.Update.Set(s => s.Estado, "ABIERTA");

                _context.EstadoCaja.UpdateOneAsync(filter, update);

                if (movimiento.Id == null) {
                    throw new Exception();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return false;

            }
        }

        public bool CerrarCaja(MovimientoModel movimiento)
        {
            try
            {
                var depositos = _context.Movimientos.Find(x => x.Creado == DateTime.Today && x.Tipo == "DEPOSITO").ToList();
                var retiros = _context.Movimientos.Find(x => x.Creado == DateTime.Today && x.Tipo == "RETIRO").ToList();
                var aperturaInicial = _context.Movimientos.Find(x => x.Creado == DateTime.Today && x.Tipo == "APERTURA").FirstOrDefault();
                var ventas = _context.Ventas.Find(x => x.Creado == DateTime.Today && x.TipoTransaccion == "EFECTIVO").ToList();

                var montoDepositos = depositos.Sum(x => x.Monto);
                var montoRetiros = retiros.Sum(x => x.Monto);
                var montoVentas = ventas.Sum(x => x.Total);

                var total = montoDepositos + montoVentas + aperturaInicial.Monto - montoRetiros;

                var movimientoCierre = new MovimientoModel();
                movimientoCierre.Monto = total;
                movimientoCierre.Tipo = "CIERRE";
                movimientoCierre.Creado = DateTime.Now;
                movimientoCierre.Descripcion = "CIERRE AUTOMATICO DE CAJA";
                movimientoCierre.FechaMovimiento = DateTime.Now;


                _context.Movimientos.InsertOne(movimientoCierre);
                var estados = _context.EstadoCaja.Find(_ => true).FirstOrDefault();

                var filter = Builders<EstadoCaja>.Filter.Eq(s => s.Id, estados.Id);
                var update = Builders<EstadoCaja>.Update.Set(s => s.Estado, "CERRADA");

                _context.EstadoCaja.UpdateOneAsync(filter, update);

                if (movimiento.Id == null)
                {
                    throw new Exception();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return false;

            }
        }

        public IEnumerable<bool> EstadoCaja()
        {
            try
            {
                var ok = new List<bool>();
                var movimiento = _context.Movimientos.Find(d => d.Tipo == "APERTURA" 
                && d.Creado > DateTime.Today.Date
                && d.Creado < DateTime.Today.Date.AddDays(1)).FirstOrDefault();

                if (movimiento != null)
                {
                    ok.Add(true);
                    return ok;
                }
                else {
                    ok.Add(false);
                    return ok;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return new List<bool> { false };

            }
        }

        public double? AperturaInicialCaja()
        {
            try
            {
                var movimiento = _context.Movimientos.Find(d => d.Tipo == "APERTURA" 
                && d.Creado > DateTime.Today.Date
                && d.Creado < DateTime.Today.Date.AddDays(1)).FirstOrDefault();

                return movimiento.Monto;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return 0;
            }
        }

        public double? IngresosCaja()
        {
            try
            {
                double? totalDelDia=0;
                var movimiento = _context.Movimientos.Find(d => d.Tipo == "DEPOSITO" && d.Creado >= DateTime.Today.Date && d.Creado < DateTime.Today.AddDays(1)).ToList();

                var ventas = _context.Ventas.Find(d => d.Creado >= DateTime.Today.Date && d.Creado < DateTime.Today.AddDays(1)).ToList();
                var totalVentasDia = ventas.Sum(x => x.Total);

                if (movimiento.Count <= 0) return totalDelDia;
                for (var i=0; i< movimiento.Count; i++ ) {
                    totalDelDia += movimiento[i].Monto;
                }

                var total = totalDelDia + totalVentasDia;
                return Math.Round(Convert.ToDouble(total), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return 0;

            }
        }

        public double? EgresosCaja()
        {
            try
            {
                double? totalDelDia = 0;
                var movimiento = _context.Movimientos.Find(d => d.Tipo == "RETIRO" && d.Creado >= DateTime.Today.Date && d.Creado < DateTime.Today.AddDays(1)).ToList();

                if (movimiento.Count <= 0) return totalDelDia;
                for (var i = 0; i < movimiento.Count; i++)
                {
                    totalDelDia += movimiento[i].Monto;
                }

                return Math.Round(Convert.ToDouble(totalDelDia));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return 0;
            }
        }


        public KeyValuePair<int, double> VentasDelDia()
        {
            var list = new KeyValuePair<int, double>();
            try
            {
                double totalDelDia = 0;
                var movimiento = _context.Ventas.Find(d => d.Creado >= DateTime.Today.Date && d.Creado < DateTime.Today.AddDays(1)).ToList();

                if (movimiento.Count <= 0) return list;
                for (var i = 0; i < movimiento.Count; i++)
                {
                    totalDelDia += (double)movimiento[i].Total;
                }

                list = new KeyValuePair<int, double>(movimiento.Count, Math.Round(totalDelDia, 2));

                return list;
                //return Math.Round(Convert.ToDouble(totalDelDia));
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                return list;
            }
        }

        public bool AddMovimiento(MovimientoModel movimiento)
        {
            var _return = false;
            try
            {
                _context.Movimientos.InsertOneAsync(movimiento);
                
                _return= true;
            }
            catch (Exception ex)
            {
                _return = false;
                Console.WriteLine("ERROR");
            }

            return _return;
        }

        public Task<MovimientoModel> GetCierreCajaCalculo()
        {
            var depositos = _context.Movimientos.Find(x => x.Creado > DateTime.Today.Date
            && x.Creado < DateTime.Today.Date.AddDays(1) 
            && x.Tipo == "DEPOSITO").ToList();
            var retiros = _context.Movimientos.Find(x => x.Creado > DateTime.Today.Date
            && x.Creado < DateTime.Today.Date.AddDays(1) 
            && x.Tipo == "RETIRO").ToList();
            var aperturaInicial = _context.Movimientos.Find(x => x.Creado > DateTime.Today.Date 
            && x.Creado < DateTime.Today.Date.AddDays(1)
            && x.Tipo == "APERTURA").FirstOrDefault();
            var ventas = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
            && x.Creado < DateTime.Today.Date.AddDays(1)
            && x.TipoTransaccion == "EFECTIVO").ToList();

            var montoDepositos = depositos.Sum(x => x.Monto);
            var montoRetiros = retiros.Sum(x => x.Monto);
            var montoVentas = ventas.Sum(x => x.Total);

            var total = montoDepositos + montoVentas + aperturaInicial.Monto - montoRetiros;

            var movimientoCierre = new MovimientoModel();
            movimientoCierre.Monto = total;
            movimientoCierre.Tipo = "CIERRE";
            movimientoCierre.Creado = DateTime.Now;
            movimientoCierre.Descripcion = "CIERRE AUTOMATICO DE CAJA";
            movimientoCierre.FechaMovimiento = DateTime.Now;

            return Task.Run(() => movimientoCierre);
        }
    }
}
