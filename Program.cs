using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Registry;
using Registry.Generator;


var builder = WebAssemblyHostBuilder.CreateDefault(args);


// var registryDirectory =
//     Directory.GetCurrentDirectory();

// Console.WriteLine(
//     $"Registry: {registryDirectory}"
// );

// var generator = new IndexGenerator();

// await generator.GenerateAsync(registryDirectory);

// Console.WriteLine(
//     "✓ Registry index generated successfully."
// );
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

 



builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
