using Fluxor.Persist.Middleware;
using Heroplate.Admin.Application;
using Heroplate.Admin.Infrastructure;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.AddConfiguration(
    builder.Configuration.GetSection("Logging"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddClientServices(builder.Configuration);

builder.Services.AddFluxor(opts => opts
    .ScanAssemblies(typeof(Program).Assembly)
    .UsePersist(o => o.UseInclusionApproach()));

// 👇 new code
builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});

// 👆 new code

var host = builder.Build();

var layoutState = host.Services.GetRequiredService<IState<LayoutState>>();
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture =
    new CultureInfo(layoutState.Value.LanguageCode);

await host.RunAsync();