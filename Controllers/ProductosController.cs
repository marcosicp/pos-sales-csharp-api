using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DieteticaPuchiApi.Interfaces;
using DieteticaPuchiApi.Model;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;
using ExcelDataReader;

namespace DieteticaPuchiApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ProductosController : Controller
    {
        private readonly IProductosRepository _productosRepository;
        private readonly IProveedoresRepository _proveedoresRepository;

        public ProductosController(IProductosRepository productosRepository, IProveedoresRepository proveedoresRepository)
        {
            _productosRepository = productosRepository;
            _proveedoresRepository = proveedoresRepository;
        }

        [HttpGet("GetAllProductos")]
        public async Task<IEnumerable<ProductoModel>> GetAllProductos()
        {
            return await _productosRepository.GetAllProductos();
        }

        [HttpGet("GetProductoByID")]
        public async Task<ProductoModel> GetProductoByID(string id)
        {

            return await _productosRepository.GetProductoByID(id);
        }

        [HttpGet("ImportExcel")]
        public async Task<bool> ImportExcel()
        {
            var fileName = "./Users.xlsx";
            List<ProductoModel> users = new List<ProductoModel>();
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {

                    while (reader.Read()) //Each row of the file
                    {
                        users.Add(new ProductoModel
                        {
                            Cantidad = (int)reader.GetValue(0),
                            Codigo = reader.GetValue(1).ToString(),
                            Categoria = reader.GetValue(2).ToString(),
                            CodigoProv = reader.GetValue(3).ToString(),
                            PrecioVenta = (double)reader.GetValue(4),
                            Nombre = reader.GetValue(5).ToString(),
                            PrecioCompra = (double)reader.GetValue(6),
                            ProveedorNombre = reader.GetValue(7).ToString(),
                            //Proveedor = reader.GetValue(8).ToString(),
                            Creado = new DateTime()
                        });
                    }
                }
            }
            return true;
        }

        [HttpDelete("DeleteProducto/{id}")]
        public void DeleteProducto(string id)
        {
            _productosRepository.DeleteProducto(id);
        }

        [HttpPost("AddProducto")]
        public async Task<IEnumerable<ProductoModel>> AddProducto()
        {
            var proveedorList = new List<ProductoModel>();

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                var request = await reader.ReadToEndAsync();
                var producto = JsonConvert.DeserializeObject<ProductoModel>(request);
                producto.Creado = DateTime.Now;

                await _productosRepository.AddProducto(producto);

                var reqOk = producto.Id != null;

                proveedorList.Add(producto);
            }
            return proveedorList;
        }

        [HttpPost("AddCompra")]
        public async Task<bool> AddCompra()
        {
            /* COMPRAS QUE SE LE REALIZAN A LOS PROVEEDORES */
            var proveedorList = new List<ProductoModel>();
            var reqOk = false;

            try
            {
                using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    var request = await reader.ReadToEndAsync();
                    var comprasProveedor = JsonConvert.DeserializeObject<CompraProveedorModel>(request);

                    await _productosRepository.AddCompra(comprasProveedor.productosCompra);
                    await _proveedoresRepository.AddCompra(comprasProveedor);

                    reqOk = true;
                }
            }
            catch (Exception ex)
            {
                reqOk = false;
                throw ex;
            }
            
            return reqOk;
        }


        [HttpPost("UpdateProducto")]
        public async Task<ProductoModel> UpdateProducto()
        {
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                try
                {
                    var request = await reader.ReadToEndAsync();
                    var producto = JsonConvert.DeserializeObject<ProductoModel>(request);
                    producto.Editado = DateTime.Now;

                    //await _productosRepository.AddProducto(producto);

                    return await _productosRepository.UpdateProducto(producto);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR", ex.Message);
                    return null;
                }
            

                
            }
        }
    }
}
