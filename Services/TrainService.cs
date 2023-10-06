using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReservationAppApi.Configurations;
using ReservationAppApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationAppApi.Services
{
    public class TrainService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Train> _trainCollection;

        public TrainService(IMongoDatabase database)
        {
            _database = database;
            _trainCollection = _database.GetCollection<Train>("Trains");
        }

        public async Task<List<Train>> GetAsync() => await _trainCollection.Find(_ => true).ToListAsync();

        public async Task<Train> GetAsync(string id)
        {
            var filter = Builders<Train>.Filter.Eq(t => t.Id, id);
            return await _trainCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateTrain(Train train) => await _trainCollection.InsertOneAsync(train);

        public async Task UpdateTrain(Train train) => await _trainCollection.ReplaceOneAsync(filter: x => x.Id == train.Id, train);

        public async Task DeleteTrain(string id) => await _trainCollection.DeleteOneAsync(filter: x => x.Id == id);
    }
}
