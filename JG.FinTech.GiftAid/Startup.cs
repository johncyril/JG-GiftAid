using Autofac;
using JG.FinTech.GiftAid.Api.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace JG.FinTech.GiftAid.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public ILifetimeScope AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseOpenApi();
            app.UseSwaggerUi3(); // TODO load the swagger yaml in wwwroot
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var taxRate = Configuration.GetValue<decimal>(ConfigurationConstants.GiftAidTaxRate, 0);
            var donationMinValue = Configuration.GetValue<decimal>(ConfigurationConstants.DonationMinumimValue, 0);
            var donationMaxValue = Configuration.GetValue<decimal>(ConfigurationConstants.DonationMaxmimValue, 0);
            builder.RegisterModule(new GiftAidModule(taxRate, donationMinValue, donationMaxValue));
        }
    }
}
