using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Localization;
using MudBlazor;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PortfolioWebsite.Client.Services;
using PortfolioWebsite.Shared.Repository;
using Microsoft.AspNetCore.Components;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddTransient<ProjectService, ProjectService>();
builder.Services.AddTransient<ImageUploadService, ImageUploadService>();
builder.Services.AddTransient<EmailService, EmailService>();
builder.Services.AddScoped(sp =>
	new HttpClient
	{
		BaseAddress = new Uri("https://localhost:7158/")
	});
builder.Services.AddSingleton<IClassProvider, BootstrapClassProvider>();
builder.Services.AddSingleton<IStyleProvider, BootstrapStyleProvider>();
builder.Services.AddSingleton<AppState, AppState>();
builder.Services.AddScoped<Blazorise.Localization.ITextLocalizerService, Blazorise.Localization.TextLocalizerService>();
builder.Services.AddMudPopoverService();
builder.Services.AddMudBlazorSnackbar(config =>
{
	config.PositionClass = Defaults.Classes.Position.BottomCenter;
	config.PreventDuplicates = true;
	config.NewestOnTop = true;
	config.ShowCloseIcon = true;
	config.VisibleStateDuration = 5000;
	config.HideTransitionDuration = 500;
	config.ShowTransitionDuration = 500;
});
builder.Services.AddMudBlazorScrollManager();
builder.Services.AddMudBlazorKeyInterceptor();
await builder.Build().RunAsync();
