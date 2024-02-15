using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {
    [BsonIgnoreExtraElements]
    public class ClienteModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }
        
        [BsonElement("activo")]
        public bool Activo { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }

        [BsonElement("cuit")]
        public string Cuit { get; set; }

        [BsonElement("cuil")]
        public string Cuil { get; set; }

        [BsonElement("razonSocial")]
        public string RazonSocial { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("telefono")]
        public string Telefono { get; set; }

        [BsonElement("direccion")]
        public string Direccion { get; set; }

        [BsonElement("dni")]
        public string Dni { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}