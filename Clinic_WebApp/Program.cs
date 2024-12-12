
using Clinic_WebApp.Repositories;
using Clinic_WebApp.Services;
using Clinic_WebApp.Repositories;
using Clinic_WebApp.Services;
using Clinic_WebApp;
using Microsoft.EntityFrameworkCore;

namespace Clinic_WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            // Connection String

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.

            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IClinicRepo, ClinicRepo>();
            builder.Services.AddScoped<IBookingRepo, BookingRepo>();

            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IClinicService, ClinicService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

    }
}
