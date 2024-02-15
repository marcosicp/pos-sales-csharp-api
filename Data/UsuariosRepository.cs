using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly MongoDbContext _context;

        public UsuariosRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<IEnumerable<UsuarioModel>> GetAllUsuarios()
        {
            try
            {
                return await _context.Usuarios.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<UsuarioModel> Login(string usuario)
        {
            try
            {
                return await _context.Usuarios.Find(user => user.Usuario == usuario).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> AddUsuario(UsuarioModel usuario)
        {
            try
            {
                await _context.Usuarios.InsertOneAsync(usuario);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUsuario(string id)
        {
            try
            {
                var actionResult = await _context.Usuarios.DeleteOneAsync(
                     Builders<UsuarioModel>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario)
        {
            try
            {
                var filter = Builders<UsuarioModel>.Filter.Eq(s => s.Id, usuario.Id);
                await _context.Usuarios.ReplaceOneAsync(filter, usuario);
                return usuario;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAllUsuarios()
        {
            try
            {
                var actionResult = await _context.Usuarios.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<UsuarioModel> GetUsuario(string usuario)
        {
            return _context.Usuarios.FindSync(user => user.Usuario == usuario && user.Usuario != null).FirstOrDefault();
        }

        public async Task<IEnumerable<UsuarioModel>> GetUsuarios(string usuario)
        {
            try
            {
                var query = _context.Usuarios.Find(user => user.Usuario == usuario);

                return await query.ToListAsync();
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
    }
}
