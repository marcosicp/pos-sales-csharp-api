using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DieteticaPuchiApi.Model
{
    public class EstadisticaModel
    {
        [BsonElement("total")]
        public double? Total { get; set; }

        [BsonElement("mes")]
        public string Mes { get; set; }
    }
}
