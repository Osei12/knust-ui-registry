using System.Text.Json;
using Registry.Models;

namespace Registry.Generator;

public sealed class IndexGenerator
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    public async Task GenerateAsync(string registryDirectory)
    {
        var componentsDirectory =
            Path.Combine(registryDirectory, "components");

        var indexPath =
            Path.Combine(registryDirectory, "index.json");

        if (!Directory.Exists(componentsDirectory))
        {
            throw new DirectoryNotFoundException(
                $"Components directory not found: {componentsDirectory}"
            );
        }

        var components = new List<RegistryComponent>();

        foreach (var directory in Directory.GetDirectories(
            componentsDirectory))
        {
            var manifestPath =
                Path.Combine(directory, "manifest.json");

            if (!File.Exists(manifestPath))
                continue;

            var json =
                await File.ReadAllTextAsync(manifestPath);

            var manifest =
                JsonSerializer.Deserialize<ComponentManifest>(
                    json,
                    _jsonOptions
                );

            if (manifest is null)
                continue;

            var directoryName =
                Path.GetFileName(directory);

            components.Add(new RegistryComponent
            {
                Name = manifest.Name,
                DisplayName = manifest.DisplayName,
                Description = manifest.Description,
                Version = manifest.Version,
                Manifest =
                    $"components/{directoryName}/manifest.json"
            });
        }

        components = components
            .OrderBy(x => x.Name)
            .ToList();

        var index = new RegistryIndex
        {
            Name = "KNUST UI",
            Version = "1.0.0",
            SchemaVersion = "1",
            Components = components
        };

        var output =
            JsonSerializer.Serialize(index, _jsonOptions);

        await File.WriteAllTextAsync(
            indexPath,
            output
        );
    }
}
// using System.Text.Json;
// using Registry.Models;

// public sealed class IndexGenerator
// {
//     private readonly JsonSerializerOptions _jsonOptions = new()
//     {
//         PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//         WriteIndented = true
//     };

//     public sealed class RegistryIndex
// {
//     public required string Name { get; init; }
//     public required string Version { get; init; }
//     public required string SchemaVersion { get; init; }
//     public required List<RegistryComponent> Components { get; init; }
// }

//     public async Task GenerateAsync(
//         string registryDirectory
//     )
//     {
//         var componentsDirectory =
//             Path.Combine(registryDirectory, "components");

//         var indexPath =
//             Path.Combine(registryDirectory, "index.json");

//         var components = new List<RegistryComponent>();

//         foreach (var directory in Directory.GetDirectories(
//             componentsDirectory))
//         {
//             var manifestPath =
//                 Path.Combine(directory, "manifest.json");

//             if (!File.Exists(manifestPath))
//                 continue;

//             var json = await File.ReadAllTextAsync(manifestPath);

//             var manifest =
//                 JsonSerializer.Deserialize<ComponentManifest>(
//                     json,
//                     _jsonOptions
//                 );

//             if (manifest is null)
//                 continue;

//             var directoryName =
//                 Path.GetFileName(directory);

//             components.Add(new RegistryComponent
//             {
//                 Name = manifest.Name,
//                 DisplayName = manifest.DisplayName,
//                 Description = manifest.Description,
//                 Version = manifest.Version,
//                 Manifest =
//                     $"components/{directoryName}/manifest.json"
//             });
//         }

//         components = components
//             .OrderBy(x => x.Name)
//             .ToList();

//         var index = new RegistryIndex
//         {
//             Name = "KNUST UI",
//             Version = "1.0.0",
//             SchemaVersion = "1",
//             Components = components
//         };

//         var output =
//             JsonSerializer.Serialize(index, _jsonOptions);

//         await File.WriteAllTextAsync(
//             indexPath,
//             output
//         );
//     }
// }