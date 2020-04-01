using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleShop.Interfaces;
using SampleShop.Queries;
using SampleShop.Services;

namespace SampleShop
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // TODO
            // Configure Dependency Injection

            IDatabase db = DatabaseFactory.CreateDatabase();
            services.Add(
                    new ServiceDescriptor(typeof(IOrdersService),
                        new OrdersService(new GetAllOrdersQuery(db),
                                          new GetAllItemsQuery(db),
                                          new GetOrderByIdQuery(db),
                                          new AddOrderQuery(db),
                                          new DeleteOrderQuery(db))
                )
            );

            services.Add(
                new ServiceDescriptor(typeof(IItemsService),
                    new ItemsService(new GetAllItemsQuery(db)))
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}
