using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IUsuariosRepository
    {
        Task<IEnumerable<UsuarioModel>> GetAllUsuarios();
        Task<bool> AddUsuario(UsuarioModel usuario);
        Task<UsuarioModel> GetUsuario(string usuario);
        Task<bool> DeleteUsuario(string id);
        Task<UsuarioModel> UpdateUsuario(UsuarioModel usuario);
    }
}
