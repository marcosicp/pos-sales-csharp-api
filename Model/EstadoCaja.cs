using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class EstadoCaja
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("idMovimiento")]
        public string IdMovimiento { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}