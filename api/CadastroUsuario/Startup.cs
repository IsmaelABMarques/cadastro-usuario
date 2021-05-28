using AutoMapper;
using CadastroUsuario.Model.Entity;
using CadastroUsuario.Model.Model;
using CadastroUsuario.Model.Repository;
using CadastroUsuario.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CadastroUsuario
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddCors(options =>
            {
                options.AddPolicy("Policy1",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                    });
                options.AddPolicy("Policy2",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });
            services.AddControllers();

            services.AddAutoMapper(new Action<IMapperConfigurationExpression>(c =>
            {
            }), typeof(Startup));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateModelAttribute));
            })
                .AddFluentValidation()
                .AddXmlSerializerFormatters();

            services.AddDbContext<RepositoryBase>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("prod")));

            services.AddTransient<UsuarioValidator>();

            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                // TODO: seria possível deixar isso em outras classes?
                cfg.CreateMap<UsuarioModel, Usuario>()
                    .ForMember(d => d.StatusId, s => s.MapFrom(x => x.Status.Id))
                    .ReverseMap();

                cfg.CreateMap<Usuario, UsuarioModel>()
                    .ReverseMap();

            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddTransient<UsuarioRepository>();
            services.AddTransient<StatusRepository>();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}
