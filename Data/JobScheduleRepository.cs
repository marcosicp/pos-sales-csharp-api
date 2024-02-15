using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MongoDB.Bson;
using Quartz;
using System.Linq;

namespace DieteticaPuchiApi.Data
{
    public class JobScheduleRepository : IDGJobRepository
    {
        private readonly MongoDbContext _context;

        public JobScheduleRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                try
                {
                    var depositos  = _context.Movimientos.Find(x=>x.Creado == DateTime.Today && x.Tipo == "DEPOSITO").ToList();
                    var retiros = _context.Movimientos.Find(x => x.Creado == DateTime.Today && x.Tipo == "RETIRO").ToList();
                    var aperturaInicial = _context.Movimientos.Find(x => x.Creado == DateTime.Today && x.Tipo == "APERTURA").FirstOrDefault();
                    var ventas = _context.Ventas.Find(x => x.Creado == DateTime.Today && x.TipoTransaccion == "EFECTIVO").ToList();

                    var montoDepositos = depositos.Sum(x => x.Monto);
                    var montoRetiros = retiros.Sum(x => x.Monto);
                    var montoVentas = ventas.Sum(x=>x.Total);

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
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR");
                    return false;

                }
            });
        }
    }
}
