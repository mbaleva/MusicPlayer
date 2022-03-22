namespace MusicPlayer.Web.Server
{
    using MusicPlayer.Common;
    using MusicPlayer.Data;
    using MusicPlayer.Data.Models;
    using MusicPlayer.Services.DataProviders;
    using MusicPlayer.Services.Users;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                   options => options.UseSqlServer(
                       this.Configuration
                            .GetConnectionString("DefaultConnection"),
                       sqlOptions => sqlOptions
                            .MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services
                .Configure<ApplicationSettings>(this.Configuration
                    .GetSection(nameof(ApplicationSettings)));
            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            var secret = this.Configuration
                .GetSection(nameof(ApplicationSettings))
                .GetValue<string>(nameof(ApplicationSettings.Secret));
            
            var key = Encoding.ASCII.GetBytes(secret);

            services
               .AddAuthentication(authentication =>
               {
                   authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(bearer =>
               {
                   bearer.RequireHttpsMetadata = false;
                   bearer.SaveToken = true;
                   bearer.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(key),
                       ValidateIssuer = false,
                       ValidateAudience = false
                   };
               });

            services.AddControllers();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddTransient<IYouTubeDataProviderService, YouTubeDataProviderService>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
