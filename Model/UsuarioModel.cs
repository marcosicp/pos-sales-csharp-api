using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {
    [BsonIgnoreExtraElements]
    public class UsuarioModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]
        public string Apellido { get; set; }

        [BsonElement("fechaNacimiento")]
        public decimal FechaNacimiento { get; set; }

        [BsonElement("usuario")]
        public string Usuario { get; set; }
        
        public string Token { get; set; }

        [BsonElement("rol")]
        public bool Rol { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}

