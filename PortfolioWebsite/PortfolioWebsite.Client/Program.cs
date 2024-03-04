using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Localization;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioWebsite.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddSingleton<IClassProvider, BootstrapClassProvider>();
builder.Services.AddSingleton<IStyleProvider, BootstrapStyleProvider>();
builder.Services.AddSingleton<AppState, AppState>();
builder.Services.AddScoped<Blazorise.Localization.ITextLocalizerService, Blazorise.Localization.TextLocalizerService>();
builder.Services.AddMudPopoverService();
builder.Services.AddMudBlazorScrollManager();
builder.Services.AddMudBlazorKeyInterceptor();
await builder.Build().RunAsync();
