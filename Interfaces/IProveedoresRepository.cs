using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IProveedoresRepository
    {
        Task<bool> AddProveedor(ProveedorModel proveedor);
        Task<bool> DeleteProveedor(string id);
        Task<IEnumerable<ProveedorModel>> GetAllProveedores();
        Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedores();
        Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedoresCC();
        Task<ProveedorModel> UpdateProveedor(ProveedorModel proveedor);
        Task<bool> AddCompra(CompraProveedorModel proveedor);
    }
}
