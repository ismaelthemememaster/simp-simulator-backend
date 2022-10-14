﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using simp_simulator_models;
using simp_simulator_models.BsonMappers;

namespace simp_simulator_backend.Services;

public class JobsService
{
    private readonly IMongoCollection<Job> _jobsCollection;

    public JobsService(
        IOptions<SimpSimulatorDatabaseSettings> simpSimulatorDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            simpSimulatorDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            simpSimulatorDatabaseSettings.Value.DatabaseName);

        _jobsCollection = mongoDatabase.GetCollection<Job>(
            simpSimulatorDatabaseSettings.Value.JobsCollectionName);
    }

    public async Task<List<Job>> GetAsync() =>
        await _jobsCollection.Find(_ => true).ToListAsync();

    public async Task<Job?> GetAsync(string id) =>
        await _jobsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Job newJob) =>
        await _jobsCollection.InsertOneAsync(newJob);

    public async Task UpdateAsync(string id, Job updatedJob) =>
        await _jobsCollection.ReplaceOneAsync(x => x.Id == id, updatedJob);

    public async Task RemoveAsync(string id) =>
        await _jobsCollection.DeleteOneAsync(x => x.Id == id);
}
