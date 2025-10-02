using System;

using CodeChallenge.Data;
using CodeChallenge.Repositories;
using CodeChallenge.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeChallenge.Config
{
    public class App
    {
        public WebApplication Configure(string[] args)
        {
            args ??= Array.Empty<string>();

            var builder = WebApplication.CreateBuilder(args);

            builder.UseEmployeeDB();
            
            AddServices(builder.Services);

            var app = builder.Build();

            var env = builder.Environment;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedEmployeeDB();
            }

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        private void AddServices(IServiceCollection services)
        {

            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ICompensationService, CompensationService>();
            services.AddScoped<ICompensationRepository, CompensationRepository>();
            
            services.AddDbContext<CompensationContext>(options =>
                options.UseInMemoryDatabase("CompensationDB"));

            services.AddControllers();
        }

        private void SeedEmployeeDB()
        {
            var dbContextOptions = new DbContextOptionsBuilder<EmployeeContext>().UseInMemoryDatabase("EmployeeDB").Options;
            var employeeContext = new EmployeeContext(dbContextOptions);
            new EmployeeDataSeeder(employeeContext).Seed().Wait();
        }
    }
}
