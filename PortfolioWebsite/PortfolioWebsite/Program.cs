using PortfolioWebsite.Client.Pages;
using PortfolioWebsite.Components;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Blazorise.Localization;
using MudBlazor.Services;
using MudBlazor;
using PortfolioWebsite.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<IClassProvider, BootstrapClassProvider>();
builder.Services.AddSingleton<IStyleProvider, BootstrapStyleProvider>();
builder.Services.AddSingleton<AppState, AppState>();
//builder.Services.AddSingleton<MudPopoverProvider, MudPopoverProvider>();
builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddLocalization();

builder.Services.AddScoped<Blazorise.Localization.ITextLocalizerService, Blazorise.Localization.TextLocalizerService>();
builder.Services.AddMudServices().AddMudPopoverService();

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

app.Run();
