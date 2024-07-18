

using Badminton.DataAccess.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace BadmintonCourtBooking
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/AccountPages/Login";
                options.AccessDeniedPath = "/AccountPages/Login";
            });
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<BadmintonManagmentDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"))
                .LogTo(Console.WriteLine, LogLevel.Information);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //var sessionDurationConfig = builder.Configuration.GetSection("Settings:SessionDuration").Value;
            //var sessionDuration = double.TryParse(sessionDurationConfig, out double duration) ? duration : 10.0;

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BadmintonManagmentDBContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapGet("/", () => Results.Redirect("/Index"));
            app.MapRazorPages();

            app.Run();
        }
    }
}