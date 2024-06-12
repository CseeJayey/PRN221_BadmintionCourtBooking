

using Badminton.DataAccess.Models;
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            builder.Services.AddDbContext<BadmintonManagmentDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DB"))
                .LogTo(Console.WriteLine, LogLevel.Information);
                //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}