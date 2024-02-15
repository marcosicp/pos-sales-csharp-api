using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
    {
    [BsonIgnoreExtraElements]
    public class ConfiguracionModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("efectivo")]
        public double Efectivo { get; set; }

        [BsonElement("unacuota")]
        public double UnaCuota { get; set; }
        
        [BsonElement("TresCuotas")]
        public double TresCuotas{ get; set; }

        [BsonElement("transferencia")]
        public double Transferencia { get; set; }

        [BsonElement("debito")]
        public double Debito { get; set; }
        
        [BsonElement("mercadoPago")]
        public double MercadoPago { get; set; }

        [BsonElement("cuentaCorriente")]
        public double CuentaCorriente { get; set; }

        [BsonElement("editado")]
        public DateTime? Editado { get; set; }
    }
}

