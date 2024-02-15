using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IConfiguracionRepository
    {
        Task<bool> UpdateConfiguracion(ConfiguracionModel config);
        Task<ConfiguracionModel> GetAllConfiguracion();
    }
}
