﻿using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MyMovies.Model;
using AutoMapper;
using MyMovies.CentralHub;

namespace MyMovies
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Registering Azure SignalR service
            services.AddSignalR();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:3000/",
                                        "https://mymoviesmsa.azurewebsites.net",
                                        "https://mymoviesmsa.azurewebsites.net");
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<MyMoviesContext>();
            services.AddAutoMapper(typeof(Startup));
            //Registering Azure SignalR service
            services.AddSignalR();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Scribr API",
                    Version = "v1",
                    Description = "A web API providing a custom toolkit for YouTube video transcription.",
                    Contact = new Contact
                    {
                        Name = "MSA 2019 - Phase 2",
                        Email = "nzmsa@microsoft.com",
                        Url = "https://github.com/NZMSA/2018-Phase-2"
                    },
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Make sure the CORS middleware is ahead of SignalR.
            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000", "https://mymoviesmsa.azurewebsites.net", "https://mymoviesmsa.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
            // SignalR
            app.UseFileServer();
            app.UseSignalR(routes =>
            {
                routes.MapHub<SignalrHub>("/hub");
            });
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ScribrAPI V1");
                c.RoutePrefix = string.Empty; // launch swagger from root
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(MyAllowSpecificOrigins);

        }
    }
}
