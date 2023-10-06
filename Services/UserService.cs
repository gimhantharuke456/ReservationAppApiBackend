using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReservationAppApi.Configurations;
using ReservationAppApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationAppApi.Services
{
    public class UserService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<User> _userCollection;

        public UserService(IMongoDatabase database)
        {
            _database = database;
            _userCollection = _database.GetCollection<User>("Users");
        }

        public async Task<List<User>> GetAsync() => await _userCollection.Find(_ => true).ToListAsync();

        public async Task<User> GetAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateUser(User user) => await _userCollection.InsertOneAsync(user);

        public async Task UpdateUser(User user) => await _userCollection.ReplaceOneAsync(filter: x => x.Id == user.Id, user);

        public async Task DeleteUser(string id) => await _userCollection.DeleteOneAsync(filter: x => x.Id == id);
        public async Task<User> SignInAsync(string username, string password)
        {
            var filter = Builders<User>.Filter.Eq(u => u.NIC, username) & Builders<User>.Filter.Eq(u => u.Password, password);
            return await _userCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
