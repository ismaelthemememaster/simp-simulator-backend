using simp_simulator_models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using simp_simulator_models.BsonMappers;

namespace simp_simulator_backend.Services;

public class SimpsService
{
    private readonly IMongoCollection<Simp> _simpsCollection;

    public SimpsService(
        IOptions<SimpSimulatorDatabaseSettings> simpSimulatorDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            simpSimulatorDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            simpSimulatorDatabaseSettings.Value.DatabaseName);

        _simpsCollection = mongoDatabase.GetCollection<Simp>(
            simpSimulatorDatabaseSettings.Value.SimpsCollectionName);
    }

    public async Task<List<Simp>> GetAsync() =>
        await _simpsCollection.Find(_ => true).ToListAsync();

    public async Task<Simp?> GetAsync(string id) =>
        await _simpsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Simp newSimp) =>
        await _simpsCollection.InsertOneAsync(newSimp);

    public async Task UpdateAsync(string id, Simp updatedSimp) =>
        await _simpsCollection.ReplaceOneAsync(x => x.Id == id, updatedSimp);

    public async Task RemoveAsync(string id) =>
        await _simpsCollection.DeleteOneAsync(x => x.Id == id);
}
