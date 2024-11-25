using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleAuthApp.Utility.DTOs
{
    public class User
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;

    }

    public class UserDB : User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;
        public byte[] PasswordSalt { get; set; } = null!;
    }

    public class UserRequest
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
