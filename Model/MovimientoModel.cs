using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace DieteticaPuchiApi.Model
{

    [BsonIgnoreExtraElements]
    public class MovimientoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("tipo")]
        public string Tipo { get; set; }

        [BsonElement("monto")]
        public double? Monto { get; set; }

        [BsonElement("creado")]
        public DateTime? Creado { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }

        [BsonElement("descripcion")]
        public string Descripcion { get; set; }

        [BsonElement("fechaMovimiento")]
        public DateTime? FechaMovimiento { get; set; }

        [BsonElement("usuario")]
        public UsuarioModel Usuario { get; set; }

        public string _Usuario { get; set; }

        [BsonElement("email")]
        public UsuarioModel Email { get; set; }

    }
}

