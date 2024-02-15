using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IClientesRepository
    {
        Task<bool> AddCliente(ClienteModel cliente);
        Task<bool> DeleteCliente(string id);
        Task<IEnumerable<ClienteModel>> GetAllClientes();
        Task<ClienteModel> UpdateCliente(ClienteModel cliente);
    }
}
