using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IComprasProveedoresRepository
    {
        
        Task<IEnumerable<CompraProveedorModel>> GetAllComprasProveedores();
        Task<CompraProveedorModel> UpdateCompraProveedor(CompraProveedorModel compraProveedor);
        Task<bool> AddCompraProveedor(CompraProveedorModel proveedor);
        Task<CompraProveedorModel> GetCompraProveedoresByID(string compraProveedorId);
    }
}
