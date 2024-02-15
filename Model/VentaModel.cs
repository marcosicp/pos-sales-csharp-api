using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class VentaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("imagenUrl")]
        public string ImagenUrl { get; set; }

        [BsonElement("direccion")]
        public string Direccion { get; set; }

        [BsonElement("observacion")]
        public string Observacion { get; set; }
        
        [BsonElement("tipoTransaccion")]
        public string TipoTransaccion  { get; set; }

        [BsonElement("clienteId")]
        public string ClienteId { get; set; }

        [BsonElement("descuento")]
        public string Descuento { get; set; }

        [BsonElement("cliente")]
        public ClienteModel Cliente { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("pagoCon")]
        public double? PagoCon { get; set; }

        [BsonElement("productos")]
        public IEnumerable<ProductoModel> ProductosVenta { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }

        [BsonElement("total")]
        public double? Total { get; set; }

        [BsonElement("creado")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? Creado { get; set; }

        [BsonElement("fechaPedido")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime? FechaPedido { get; set; }

        [BsonElement("pendiente")]
        public bool Pendiente { get; set; }
    }
}