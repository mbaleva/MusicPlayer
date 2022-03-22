namespace MusicPlayer.Web.Client
{
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using MusicPlayer.Web.Client.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(new HttpClient());
            builder.Services.AddSingleton<IAppState, AppState>();
            //builder.Services.AddSingleton<IMediaPlayer, MediaPlayer>();
            builder.Services.AddTransient<IApiClient, ApiClient>();

            await builder.Build().RunAsync();
        }
    }
}
