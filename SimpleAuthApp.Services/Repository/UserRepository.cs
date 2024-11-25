using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SimpleAuthApp.Utility.DTOs;
using SimpleAuthApp.Services;

namespace SimpleAuthApp.Services.Repository
{
    public class UserRepository
    {
        private readonly IMongoCollection<UserDB> _usersCollection;

        public UserRepository(IOptions<DatabaseSettings> dbSettings)
        {
            var client = new MongoClient(dbSettings.Value.ConnectionString);
            var database = client.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = database.GetCollection<UserDB>("Users");
        }

        // Get a single user by username
        public async Task<UserDB?> GetByUsernameAsync(string username)
        {
            return await _usersCollection.Find(user => user.Username == username).FirstOrDefaultAsync();
        }

        public async Task<UserDB?> GetByEmailAsync(string email)
        {
            return await _usersCollection.Find(user => user.Email == email).FirstOrDefaultAsync();
        }

        // Add a new user
        public async Task CreateAsync(UserDB newUser)
        {
            await _usersCollection.InsertOneAsync(newUser);
        }

        // Update an existing user
        public async Task UpdateAsync(string username, UserDB updatedUser)
        {
            await _usersCollection.ReplaceOneAsync(user => user.Username == username, updatedUser);
        }

        // Delete a user by username
        public async Task DeleteAsync(string username)
        {
            await _usersCollection.DeleteOneAsync(user => user.Username == username);
        }
    }
}
