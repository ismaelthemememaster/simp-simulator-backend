namespace simp_simulator_models;

public class SimpSimulatorDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string SimpsCollectionName { get; set; } = null!;

    public string JobsCollectionName { get; set; } = null!;
}
