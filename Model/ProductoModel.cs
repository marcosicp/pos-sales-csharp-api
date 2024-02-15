using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DieteticaPuchiApi.Model
{

    [BsonIgnoreExtraElements]
    public class ProductoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("codigo")]
        public string Codigo { get; set; }

        [BsonElement("codigoProv")]
        public string CodigoProv { get; set; }
        
        [BsonIgnore]
        [BsonElement("codigoProv")]
        public ProveedorModel Proveedor { get; set; }

        [BsonElement("proveedorNombre")]
        public string ProveedorNombre { get; set; }

        [BsonElement("imagenUrl")]
        public string ImagenUrl { get; set; }

        [BsonElement("precioVenta")]
        public double PrecioVenta { get; set; }

        [BsonElement("precioCompra")]
        public double PrecioCompra { get; set; }

        [BsonElement("categoria")]
        public string Categoria { get; set; }

        [BsonElement("cantidad")]
        public double Cantidad { get; set; }

        [BsonElement("cantidadComprada")]
        public double CantidadComprada { get; set; }

        //[BsonElement("peso")]
        //public double Peso { get; set; }

        [BsonElement("ganancia")]
        public double Ganancia { get; set; }

        [BsonElement("fechaVencimiento")]
        public DateTime? FechaVencimiento { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}