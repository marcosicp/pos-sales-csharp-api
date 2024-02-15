using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class ClientesRepository : IClientesRepository
    {
        private readonly MongoDbContext _context;

        public ClientesRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<bool> AddCliente(ClienteModel clientes)
        {
            try
            {
                clientes.Activo = true;
                clientes.Creado = DateTime.Now;

                await _context.Clientes.InsertOneAsync(clientes);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ClienteModel>> GetAllClientes()
        {
            try
            {
                return await _context.Clientes.Find(_ => true).ToListAsync();
            }
            catch (Exception)
            {
                // log or manage the exception
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> DeleteCliente(string id)
        {
            try
            {
              
                var actionResult = await _context.Clientes.DeleteOneAsync(
                    Builders<ClienteModel>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged
                       && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<ClienteModel> UpdateCliente(ClienteModel cliente)
        {
            try
            {
                var filter = Builders<ClienteModel>.Filter.Eq(s => s.Id, cliente.Id);
                await _context.Clientes.ReplaceOneAsync(filter, cliente);
                return cliente;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
