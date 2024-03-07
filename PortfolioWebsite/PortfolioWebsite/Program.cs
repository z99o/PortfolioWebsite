using PortfolioWebsite.Client.Pages;
using PortfolioWebsite.Components;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.Localization;
using MudBlazor.Services;
using MudBlazor;
using PortfolioWebsite.Client.Services;
using PortfolioWebsite.Shared.Repository;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);

var uriPath = "http://localhost:5187";
//var mongoDBSettings = builder.Configuration.GetSection("MongoDBSettings").Get<MongoDBSettings>();
//builder.Services.AddScoped(service => new MongoDBService(mongoDBSettings));

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddControllers();
builder.Services.AddSingleton<IClassProvider, BootstrapClassProvider>();
builder.Services.AddSingleton<IStyleProvider, BootstrapStyleProvider>();
builder.Services.AddSingleton<AppState, AppState>();
builder.Services.AddTransient<ProjectService, ProjectService>();
builder.Services.AddTransient<ImageUploadService, ImageUploadService>();
//builder.Services.AddSingleton<IPopoverService, MudPopoverProvider>();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddLocalization();

builder.Services.AddScoped<Blazorise.Localization.ITextLocalizerService, Blazorise.Localization.TextLocalizerService>();
builder.Services.AddMudServices().AddMudPopoverService();

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
builder.Services.AddScoped(sp =>
	new HttpClient
	{
        BaseAddress = new Uri("http://localhost:5187"),
    });
builder.Services.AddHttpClient("myHttpClient", client =>
{
	client.BaseAddress = new Uri(uriPath);
	client.DefaultRequestHeaders.Clear();
	client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
}
)
.ConfigurePrimaryHttpMessageHandler(() =>
{
	return new HttpClientHandler()
	{
		UseDefaultCredentials = true,
		AllowAutoRedirect = false
	};

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PortfolioWebsite.Client._Imports).Assembly);
app.MapControllers();
app.Run();
