using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {

    [BsonIgnoreExtraElements]
    public class EventAction
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("label")]
        public string Label { get; set; }
        
        [BsonElement("cssClass")]
        public bool CssClass { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}

