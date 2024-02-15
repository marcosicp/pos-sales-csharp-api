using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class ProveedoresRepository : IProveedoresRepository
    {
        private readonly MongoDbContext _context;

        public ProveedoresRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<bool> AddProveedor(ProveedorModel proveedor)
        {
            try
            {
                await _context.Proveedores.InsertOneAsync(proveedor);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public async Task<IEnumerable<ProveedorModel>> GetAllProveedores()
        {
            try
            {
                return await _context.Proveedores.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> DeleteProveedor(string id)
        {
            try
            {
                var actionResult = await _context.Proveedores.DeleteOneAsync(
                    Builders<ProveedorModel>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<ProveedorModel> UpdateProveedor(ProveedorModel proveedor)
        {
            try
            {
                var filter = Builders<ProveedorModel>.Filter.Eq(s => s.Id, proveedor.Id);
                await _context.Proveedores.ReplaceOneAsync(filter, proveedor);
                return proveedor;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddCompra(CompraProveedorModel compraProveedor)
        {
            try
            {
                if(compraProveedor.TipoTransaccion=="CUENTA CORRIENTE"){
                    compraProveedor.Pendiente = true;
                }
                await _context.CompraProveedores.InsertOneAsync(compraProveedor);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedores()
        {
            try
            {
                return await _context.CompraProveedores.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedoresCC()
        {
            try
            {
                return await _context.CompraProveedores.Find(x => x.TipoTransaccion == "CUENTA CORRIENTE" && x.Pendiente == true).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
