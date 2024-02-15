using System;
using MongoDB.Driver;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;

namespace DieteticaPuchiApi.Data
{
    public class ConfiguracionRepository : IConfiguracionRepository
    {
        private readonly MongoDbContext _context;

        public ConfiguracionRepository(IOptions<Settings> settings)
        {
            _context = new MongoDbContext(settings);
        }



        public async Task<bool> UpdateConfiguracion(ConfiguracionModel config)
        {
            try
            {
                var filter = Builders<ConfiguracionModel>.Filter.Eq(s => s.Id, config.Id);
                await _context.Configuracion.ReplaceOneAsync(filter, config);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<ConfiguracionModel> GetAllConfiguracion()
        {
            try
            {
                return await _context.Configuracion.Find(_ => true).SingleOrDefaultAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                throw ex;
            }
        }

        
    }
}
