using DieteticaPuchiApi.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Interfaces
{
    public interface IProductosRepository
    {
        Task<bool> AddProducto(ProductoModel producto);
        Task<IEnumerable<ProductoModel>> GetAllProductos();
        Task<ProductoModel> GetProductoByID(string id);
        Task<bool> DeleteProducto(string id);
        Task<ProductoModel> UpdateProducto(ProductoModel producto);
        Task<bool> VerifyProductosStock(IEnumerable<ProductoModel> productosPedidos);
        Task<bool> AddCompra(List<ProductoModel> productos);
        Task<bool> ReducirStock(IEnumerable<ProductoModel> productos);
    }
}
