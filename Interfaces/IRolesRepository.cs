using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IRolesRepository
    {
        Task<IEnumerable<RolModel>> GetAllRoles();

        Task<RolModel> GetRolById(string id);

        Task<RolModel> AddRol(RolModel rol);

        Task<bool> UpdateRol(RolModel rol);

        Task<bool> DeleteRol(string id);

        Task<bool> DeleteAllRol();
    }
}