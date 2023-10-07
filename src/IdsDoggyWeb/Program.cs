using IdentityServer4.Services;
using IdsDoggyWeb.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace IdsDoggyWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSingleton<Services.ITokenService, TokenService>();
            builder.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer", options =>
                {
                    options.ApiName = builder.Configuration["BasicIdsConfig:ApiName"];
                    options.Authority = builder.Configuration["BasicIdsConfig:Authority"];
                })
                .AddOpenIdConnect("oidc", options =>
                {
                    options.ClientId = builder.Configuration["BasicIdsConfig:ApiName"];
                    options.Authority = builder.Configuration["BasicIdsConfig:Authority"];
                    options.Events = new OpenIdConnectEvents
                    {
                        OnRedirectToIdentityProvider = async context =>
                        {
#if DEBUG
                            context.ProtocolMessage.RedirectUri = $"{context.HttpContext.Request.Scheme}://{context.HttpContext.Request.Host}";
#else
                            //TODO: Redirect customiziado para definir hots do bne e forçar volta para rota amigavel
                            context.ProtocolMessage.RedirectUri = $"{urlAmbiente}{callbackPath}";
#endif
                            await Task.CompletedTask;
                        },
                        OnRemoteFailure = async context => await Task.CompletedTask
                    };
                });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}