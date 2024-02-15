using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Data
{
    public class VentasRepository : IVentasRepository
    {
        private readonly MongoDbContext _context;

        public VentasRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<IEnumerable<VentaModel>> GetAllVentas()
        {
            try
            {
                return await _context.Ventas.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<IEnumerable<VentaModel>> GetAllEntregas()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<VentaModel> AddVenta(VentaModel ventas)
        {
            try
            {
                await _context.Ventas.InsertOneAsync(ventas);
                return ventas;
            }
            catch (Exception)
            {
                // SI ES NULL ES UN ERROR
                return null;
            }
        }

        public async Task<VentaModel> UpdateVenta(VentaModel venta)
        {
            try
            {
                var filter = Builders<VentaModel>.Filter.Eq(s => s.Id, venta.Id);
                await _context.Ventas.ReplaceOneAsync(filter, venta);
                return venta;
            }
            catch (Exception)
            {
                return null;
            }
        }



        #region VentasDelDia

        public async Task<double?> GetTotalVentasPedidosYaHoy()
        {
            var todasLasVentasPYHoy = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                  && x.Creado < DateTime.Today.Date.AddDays(1)
                  && x.TipoTransaccion == "PEDIDOS YA").ToList();

            return Math.Round((double)todasLasVentasPYHoy.Sum(x => x.Total), 2);
        }


        public async Task<double?> GetTotalVentasHoy()
        {
            var todasLasVentasHoy = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)).ToList();

            return Math.Round((double)todasLasVentasHoy.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasMercadoPagoHoy()
        {
            var todasLasVentasHoy = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
                    && x.TipoTransaccion == "MERCADO PAGO").ToList();

            return Math.Round((double)todasLasVentasHoy.Sum(x => x.Total), 2);
        }
        public async Task<double?> GetTotalVentasDebitoHoy()
        {
            var todasLasVentasHoy = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
                    && x.TipoTransaccion == "DEBITO").ToList();

            return Math.Round((double)todasLasVentasHoy.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasTransferenciaHoy()
        {
            var todasLasVentasHoy = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
                    && x.TipoTransaccion == "TRANSFERENCIA").ToList();

            return Math.Round((double)todasLasVentasHoy.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasTresCuotasHoy()
        {
            var todasLasVentasTresCuotas = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
                && x.TipoTransaccion == "3 CUOTAS").ToList();

            return Math.Round((double)todasLasVentasTresCuotas.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasUnaCuotaHoy()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
               && x.TipoTransaccion == "1 CUOTA").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasDebitHoy()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
               && x.TipoTransaccion == "DEBITO").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasEfectivoHoy()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
               && x.TipoTransaccion == "EFECTIVO").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasCuentaCorrienteHoy()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > DateTime.Today.Date
                    && x.Creado < DateTime.Today.Date.AddDays(1)
               && x.TipoTransaccion == "CUENTA CORRIENTE").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        #endregion VentasDelDia

        #region VentasDelMes

        public async Task<double?> GetTotalVentasMes()
        {
            var todasLasVentasMes = _context.Ventas.Find(
                x => x.Creado > GetMonthMin() &
                x.Creado < GetMonthMax()).ToList();

            return Math.Round((double)todasLasVentasMes.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasTransferenciaMes()
        {
            var todasLasVentasTransferencia = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
                && x.TipoTransaccion == "TRANSFERENCIA").ToList();

            return Math.Round((double)todasLasVentasTransferencia.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasMercadoPagoMes()
        {
            var todasLasVentasMP = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
               x.Creado < GetMonthMax()
                && x.TipoTransaccion == "MERCADO PAGO").ToList();

            return Math.Round((double)todasLasVentasMP.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasTresCuotasMes()
        {
            var todasLasVentasTresCuotas = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
               x.Creado < GetMonthMax()
                && x.TipoTransaccion == "3 CUOTAS").ToList();

            return Math.Round((double)todasLasVentasTresCuotas.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasUnaCuotaMes()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
               && x.TipoTransaccion == "1 CUOTA").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasDebitoMes()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
               && x.TipoTransaccion == "DEBITO").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasEfectivoMes()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
               && x.TipoTransaccion == "EFECTIVO").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasCuentaCorrienteMes()
        {
            var todasLasVentasUnaCuota = _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
               && x.TipoTransaccion == "CUENTA CORRIENTE").ToList();

            return Math.Round((double)todasLasVentasUnaCuota.Sum(x => x.Total), 2);
        }

        public async Task<double?> GetTotalVentasPedidosYaMes()
        {
            var todasLasVentasPY= _context.Ventas.Find(x => x.Creado > GetMonthMin() &
              x.Creado < GetMonthMax()
               && x.TipoTransaccion == "PEDIDOS YA").ToList();

            return Math.Round((double)todasLasVentasPY.Sum(x => x.Total), 2);
        }

        #endregion VentasDelMes

        public DateTime? GetMonthMin()
        {
            var min = new DateTime(DateTime.Today.Date.Year, DateTime.Today.Date.Month, 1, 00, 0, 0);
            return min;
        }

        public DateTime? GetMonthMax()
        {
            var min = GetMonthMin();//
            return min.Value.AddMonths(1).AddDays(-1);
        }

        public async Task<string> NotificarPago(VentaModel venta)
        {
            try
            {
                var filter = Builders<VentaModel>.Filter.Eq(s => s.Id, venta.Id);
                var update = Builders<VentaModel>.Update.Set(x => x.Pendiente, false)
                                                        .Set(x => x.Editado, DateTime.Now);
                await _context.Ventas.UpdateOneAsync(filter, update);
                
                return "OK";
            }
            catch (Exception ex)
            {
                return "ERROR";
            }
        }

       

      
    }
}