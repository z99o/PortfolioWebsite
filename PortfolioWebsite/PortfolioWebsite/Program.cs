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
using Microsoft.AspNetCore.Components;
[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var uriPath = "https://localhost:7158/";
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
builder.Services.AddTransient<EmailService, EmailService>();
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
        BaseAddress = new Uri(uriPath),
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
// app.UseCookiePolicy();

app.UseRouting();
app.UseAntiforgery();
// app.UseRequestLocalization();
//app.UseCors();

app.UseAuthentication();
app.UseAuthorization();
// app.UseSession();
// app.UseResponseCompression();
// app.UseResponseCaching();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PortfolioWebsite.Client._Imports).Assembly);
app.MapControllers();
app.Run();
