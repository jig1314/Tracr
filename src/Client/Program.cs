using BlazorStrap;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TabBlazor;
using Tracr.Client;
using Tracr.Client.Factories;
using Tracr.Client.MappingProfiles;
using Tracr.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Tracr.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<IUnauthorizedUserService, UnauthorizedUserService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddHttpClient<IUserService, UserService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();
builder.Services.AddHttpClient<IPropertyService, PropertyService>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Tracr.ServerAPI"));
builder.Services.AddApiAuthorization()
    .AddAccountClaimsPrincipalFactory<CustomAccountClaimsPrincipalFactory>();
builder.Services.AddBlazorStrap();
builder.Services.AddTabler();
builder.Services.AddAutoMapper(typeof(RenterProfile));

await builder.Build().RunAsync();
