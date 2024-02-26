using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TaxaApp;
using TaxaApp.Codes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
// api service
builder.Services.AddScoped<ApiService>();

// Add Blazored Modal
builder.Services.AddBlazoredModal();

// singleton = one instance for the entire application½
// scoped = one instance for each request
// transient = one instance for each time it is requested

await builder.Build().RunAsync();
