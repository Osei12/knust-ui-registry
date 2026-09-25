public sealed class RegistryIndex
{
    public required string Name { get; init; }
    public required string Version { get; init; }
    public required string SchemaVersion { get; init; }
    public required List<RegistryComponent> Components { get; init; }
}