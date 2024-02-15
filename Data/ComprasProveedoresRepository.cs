using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class ComprasProveedoresRepository : IComprasProveedoresRepository
    {
        private readonly MongoDbContext _context;

        public ComprasProveedoresRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
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

        public async Task<CompraProveedorModel> GetCompraProveedoresByID(string compraProveedorId)
        {
            try
            {
                return await _context.CompraProveedores.Find(compra => compra.Id == compraProveedorId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<CompraProveedorModel> UpdateCompraProveedor(CompraProveedorModel compraProveedor)
        {
            var filter = Builders<CompraProveedorModel>.Filter.Eq(s => s.Id, compraProveedor.Id);
            await _context.CompraProveedores.ReplaceOneAsync(filter, compraProveedor);
            return compraProveedor;
        }

        public Task<bool> AddCompraProveedor(CompraProveedorModel proveedor)
        {
            throw new NotImplementedException();
        }

    }
}
