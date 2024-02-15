using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<Settings> settings)
        {
            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                _database = client.GetDatabase(settings.Value.Database);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IMongoCollection<UsuarioModel> Usuarios => _database.GetCollection<UsuarioModel>("Usuarios");

        public IMongoCollection<ProductoModel> Productos => _database.GetCollection<ProductoModel>("Productos");

        public IMongoCollection<ConfiguracionModel> Configuracion => _database.GetCollection<ConfiguracionModel>("Configuracion");

        public IMongoCollection<RolModel> Roles => _database.GetCollection<RolModel>("Roles");

        public IMongoCollection<EstadoCaja> EstadoCaja => _database.GetCollection<EstadoCaja>("EstadoCaja");

        public IMongoCollection<MovimientoModel> Movimientos => _database.GetCollection<MovimientoModel>("Movimientos");

        public IMongoCollection<CompraProveedorModel> CompraProveedores => _database.GetCollection<CompraProveedorModel>("CompraProveedores");

        public IMongoCollection<VentaModel> Ventas => _database.GetCollection<VentaModel>("Ventas");

        public IMongoCollection<ProveedorModel> Proveedores => _database.GetCollection<ProveedorModel>("Proveedores");

        public IMongoCollection<ClienteModel> Clientes => _database.GetCollection<ClienteModel>("Clientes");
    }
}
