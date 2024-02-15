using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class CompraProveedorModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("productosCompra")]
        public List<ProductoModel> productosCompra { get; set; }

        [BsonElement("total")]
        public double? Total { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("razonSocial")]
        public string RazonSocial{ get; set; }

        [BsonElement("nombreProveedor")]
        public string NombreProveedor { get; set; }

        [BsonElement("cuil")]
        public string Cuil { get; set; }

        [BsonElement("pendiente")]
        public bool Pendiente { get; set; }

        [BsonElement("fechaLiquidacion")]
        public DateTime FechaLiquidacion { get; set; }

        [BsonElement("tipoTransaccion")]
        public string TipoTransaccion { get; set; }

        [BsonElement("proveedorId")]
        public string ProveedorId { get; set; }

        [BsonElement("fechaCompra")]
        public DateTime FechaCompra { get; set; }
    }
}
