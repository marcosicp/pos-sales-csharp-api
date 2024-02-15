using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Data
{
    public class ProductosRepository : IProductosRepository
    {
        private readonly MongoDbContext _context;

        public ProductosRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<bool> AddProducto(ProductoModel producto)
        {
            try
            {
                producto.Creado = DateTime.Now;
                producto.Editado = DateTime.Now;
                await _context.Productos.InsertOneAsync(producto);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductoModel>> GetAllProductos()
        {
            try
            {
                return await _context.Productos.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw ex;
            }
        }

        public async Task<ProductoModel> GetProductoByID(string id)
        {
            try
            {
                return await _context.Productos.Find(x => x.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw ex;
            }
        }

        public async Task<ProductoModel> UpdateProducto(ProductoModel producto)
        {
            try
            {
                var filter = Builders<ProductoModel>.Filter.Eq(s => s.Id, producto.Id);
                await _context.Productos.ReplaceOneAsync(filter, producto);
                return producto;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteProducto(string id)
        {
            try
            {
                var actionResult = await _context.Productos.DeleteOneAsync(
                    Builders<ProductoModel>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        private ObjectId GetInternalId(string id)
        {
            if (!ObjectId.TryParse(id, out var internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        public async Task<bool> VerifyProductosStock(IEnumerable<ProductoModel> productosPedidos)
        {
            var ok = true;

            try
            {
                foreach (var prod in productosPedidos)
                {
                    var cant = await _context.Productos.Find(x => x.Id == prod.Id).FirstOrDefaultAsync();

                    if (cant != null && cant.Cantidad <= 50)
                    {
                        ok = false;
                        break;
                    }
                }

                return ok;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ok;
        }

        public async Task<bool> AddCompra(List<ProductoModel> productos)
        {
            var oka = false;
            try
            {
                foreach (var item in productos)
                {
                    var prod = await _context.Productos.Find(x => x.Id == item.Id).FirstOrDefaultAsync();
                    item.Cantidad = item.CantidadComprada + prod.Cantidad;
                    item.Editado = DateTime.Now;
                    var filter = Builders<ProductoModel>.Filter.Eq(s => s.Id, item.Id);
                    await _context.Productos.ReplaceOneAsync(filter, item);
                }

                oka = true;
            }
            catch (Exception)
            {
                oka = false;
                return oka;
            }

            return oka;
        }

        public async Task<bool> ReducirStock(IEnumerable<ProductoModel> productos)
        {
            var ok = true;

            try
            {
                foreach (var prod in productos)
                {
                    var unProducto = await _context.Productos.Find(x => x.Id == prod.Id).FirstOrDefaultAsync();
                    unProducto.Cantidad = unProducto.Cantidad - prod.Cantidad;
                    unProducto.Editado = DateTime.Now;
                    var filter = Builders<ProductoModel>.Filter.Eq(s => s.Id, prod.Id);

                    await _context.Productos.ReplaceOneAsync(filter, unProducto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ok;
        }
    }
}