using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReservationAppApi.Configurations;
using ReservationAppApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationAppApi.Services
{
    public class TravelerService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Traveler> _travelerCollection;

        public TravelerService(IMongoDatabase database)
        {
            _database = database;
            _travelerCollection = _database.GetCollection<Traveler>("Travelers");
        }

        public async Task<List<Traveler>> GetAsync() => await _travelerCollection.Find(_ => true).ToListAsync();

        public async Task<Traveler> GetAsync(string id)
        {
            var filter = Builders<Traveler>.Filter.Eq(t => t.Id, id);
            return await _travelerCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateTraveler(Traveler traveler) => await _travelerCollection.InsertOneAsync(traveler);

        public async Task UpdateTraveler(Traveler traveler) => await _travelerCollection.ReplaceOneAsync(filter: x => x.Id == traveler.Id, traveler);

        public async Task DeleteTraveler(string id) => await _travelerCollection.DeleteOneAsync(filter: x => x.Id == id);

        public async Task<Traveler> SignInAsync(string username, string password)
        {
            var filter = Builders<Traveler>.Filter.Eq(u => u.NIC, username) & Builders<Traveler>.Filter.Eq(u => u.Password, password);
            return await _travelerCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Traveler> GetByNICAsync(string nic)
        {
            var filter = Builders<Traveler>.Filter.Eq(x => x.NIC, nic);
            return await _travelerCollection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
