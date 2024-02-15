using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class PedidoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("total")]
        public double? Total { get; set; }

        [BsonElement("pesoTotal")]
        public double? PesoTotal { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("clienteId")]
        public string ClienteId { get; set; }

        [BsonElement("cliente")]
        public ClienteModel Cliente { get; set; }

        [BsonElement("imagenUrl")]
        public string ImagenUrl { get; set; }

        [BsonElement("direccion")]
        public string Direccion { get; set; }

        [BsonElement("pagoCon")]
        public double? PagoCon { get; set; }

        [BsonElement("productos")]
        public IEnumerable<ProductoModel> ProductosPedidos { get; set; }
        

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }

        [BsonElement("fechaPedido")]
        public DateTime? FechaPedido { get; set; }
    }
}