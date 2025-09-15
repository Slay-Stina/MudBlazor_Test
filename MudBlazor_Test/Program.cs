using MudBlazor.Services;
using MudBlazor_Test.Components;
using MudBlazor_Test.Application;
using MudBlazor_Test.Repositories;
using MudBlazor_Test.TCPConnector;
using MudBlazor_Test_Domain.Repositories;

namespace MudBlazor_Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add MudBlazor services
            builder.Services.AddMudServices();

            // Register TcpConnection as a factory for DI
            builder.Services.AddScoped<TcpConnection>(sp => new TcpConnection("localhost", 3077)); // Adjust host/port as needed

            // Register repository and service
            builder.Services.AddScoped<ILineRepository, LineRepository>(sp => new LineRepository("localhost", 3077));
            builder.Services.AddScoped<ILineService, LineService>();

            // Add HttpClient for Blazor DI
            builder.Services.AddHttpClient();

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            app.MapControllers();

            app.Run();
        }
    }
}
