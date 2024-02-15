using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class RolModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }

        [BsonElement("estado")]
        public int Estado { get; set; }
    }
}