using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {
    [BsonIgnoreExtraElements]
    public class EventColor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("primary")]
        public string Primary { get; set; }
        
        [BsonElement("secondary")]
        public string Secondary { get; set; }

    }
}

