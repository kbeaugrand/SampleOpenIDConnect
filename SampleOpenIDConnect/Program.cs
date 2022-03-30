using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SampleOpenIDConnect;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("WebAPI",
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WebAPI"));

builder.Services.AddOidcAuthentication(options =>
{
    options.ProviderOptions.Authority = "https://dev-07461310.okta.com/oauth2/default";
    options.ProviderOptions.MetadataUrl = "https://dev-07461310.okta.com/oauth2/default/.well-known/oauth-authorization-server";
    options.ProviderOptions.ClientId = "0oa4fore7jphKD9Gi5d7";
});

await builder.Build().RunAsync();
