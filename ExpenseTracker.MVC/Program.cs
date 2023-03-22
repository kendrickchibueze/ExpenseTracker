
using Microsoft.EntityFrameworkCore;
using ExpenseTracker.DAL.Data;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using ExpenseTracker.BLL.Models;

namespace ExpenseTracker.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



           



            builder.Services.AddDbContext<ExpenseTrackerDbContext>(opts =>
            {
               

                var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];

                opts.UseSqlServer(defaultConn);

            });


          /*  Trace.Listeners.Add(new ConsoleTraceListener());

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                TraceWriter = new DiagnosticsTraceWriter(),
            };

            var serializer = JsonSerializer.Create(settings);

            // Use the serializer to serialize an object
            var obj = new TransactionVM();
            serializer.Serialize(Console.Out, obj);*/






            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBMAY9C3t2VVhkQlFacldJWXxIe0x0RWFab1d6dldMZVRBJAtUQF1hSn5Qd0NhWH1dcnFWRGBa");



            builder.Services.AddAutoMapper(Assembly.Load("ExpenseTracker.BLL"));

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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}