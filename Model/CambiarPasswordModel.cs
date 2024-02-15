using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {
    [BsonIgnoreExtraElements]
    public class CambiarPasswordModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("pass")]
        public string Pass { get; set; }

        [BsonElement("usuario")]
        public UsuarioModel Usuario { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}

