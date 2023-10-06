using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReservationAppApi.Configurations;
using ReservationAppApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReservationAppApi.Services
{
    public class ReservationService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Reservation> _reservationCollection;

        public ReservationService(IMongoDatabase database)
        {
            _database = database;
            _reservationCollection = _database.GetCollection<Reservation>("Reservations");
        }

        public async Task<List<Reservation>> GetAsync() => await _reservationCollection.Find(_ => true).ToListAsync();
        public async Task<List<Reservation>> GetByUserIdAsync(string id) => await _reservationCollection.Find(filter: x => x.CreatedBy == id).ToListAsync();

        public async Task<Reservation> GetAsync(string id)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.Id, id);
            return await _reservationCollection.Find(filter).FirstOrDefaultAsync();
        }
        public async Task<Reservation> GetAsyncTrain(string id)
        {
            var filter = Builders<Reservation>.Filter.Eq(r => r.CreatedTo, id);
            return await _reservationCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateReservation(Reservation reservation) => await _reservationCollection.InsertOneAsync(reservation);

        public async Task UpdateReservation(Reservation reservation) => await _reservationCollection.ReplaceOneAsync(filter: x => x.Id == reservation.Id, reservation);

        public async Task DeleteReservation(string id) => await _reservationCollection.DeleteOneAsync(filter: x => x.Id == id);
    }
}
