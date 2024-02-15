using MongoDB.Bson.Serialization.Attributes;

namespace DieteticaPuchiApi.Model
{
    [BsonIgnoreExtraElements]
    public class LoginRequest
    {
        public string Email { get; set; }

        public string Pass { get; set; }
    }
}
