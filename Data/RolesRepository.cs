using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using DieteticaPuchiApi.Infrastructure;
using MongoDB.Bson;

namespace DieteticaPuchiApi.Data
{
    public class RolesRepository : IRolesRepository
    {
        private readonly MongoDbContext _context;

        public RolesRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }

        public async Task<IEnumerable<RolModel>> GetAllRoles()
        {
            try
            {
                return await _context.Roles.Find(x => x.Estado.Equals(((int)Enums.Estado.Activo).ToString())).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<RolModel> GetRolById(string id)
        {
            try
            {
                return await _context.Roles.Find(x => x.Id == id && x.Estado.Equals(((int)Enums.Estado.Activo).ToString())).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<RolModel> AddRol(RolModel rol)
        {
            try
            {
                if (rol == null)
                    return null;
                
                rol.Estado = (int)Enums.Estado.Activo;

                await _context.Roles.InsertOneAsync(rol);

                return rol;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> UpdateRol(RolModel rol)
        {
            try
            {
                var actionResult = await _context.Roles.ReplaceOneAsync(x => x.Id.Equals(rol.Id), rol, new UpdateOptions { IsUpsert = false });

                return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> DeleteRol(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return false;

                var actionResult = await _context.Roles.DeleteOneAsync(Builders<RolModel>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> DeleteAllRol()
        {
            try
            {
                var actionResult = await _context.Roles.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }

        public async Task<bool> Prueba()
        {
            try
            {
                var actionResult = await _context.Roles.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw;
            }
        }
    }
}
