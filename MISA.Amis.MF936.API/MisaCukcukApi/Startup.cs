using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Misa.ApplicationCore;
using Misa.ApplicationCore.Interfaces;
using Misa.ApplicationCore.Interfaces.Base;
using Misa.ApplicationCore.Interfaces.Repository;
using Misa.ApplicationCore.Interfaces.Services;
using Misa.ApplicationCore.Services;
using Misa.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MisaCukcukApi
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

            services.AddControllers();

            //Reppository DI
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IAccountVoucherRepository, AccountVoucherRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<ICommodityRepository, CommodityRepository>();
            services.AddScoped<IAccountObjectRepository, AccountObjectRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountVoucherDetailRepository, AccountVoucherDetailRepository>();
            services.AddScoped<ICommodityUnitRepository, CommodityUnitRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<IVocativeRepository, VocativeRepository>();
            services.AddScoped<ICommodityGroupRepository, CommodityGroupRepository>();
            services.AddScoped<IAccountObjectGroupRepository, AccountObjectGroupRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            //Service DI
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountVoucherService, AccountVoucherService>();
            services.AddScoped<IWarehouseService, WarehouseService>();
            services.AddScoped<ICommodityService, CommodityService>();
            services.AddScoped<IAccountObjectService, AccountObjectService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICommodityUnitService, CommodityUnitService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IVocativeService, VocativeService>();
            services.AddScoped<ICommodityGroupService, CommodityGroupService>();
            services.AddScoped<IAccountObjectGroupService, AccountObjectGroupService>();
            services.AddScoped<IDepartmentService, DepartmentService>();

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MisaCukcukApi", Version = "v1" });
            });
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MisaCukcukApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
