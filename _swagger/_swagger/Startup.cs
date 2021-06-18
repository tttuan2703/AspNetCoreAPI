using _swagger.DataMongoDB;
using _swagger.Mapper;
using _swagger.Models;
using _swagger.Services;
using _swagger.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace _swagger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public MyDB dbClient = null;
        public static AutoMapperAccount map_account;
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                //Map AccountView to Accounts
                map_account = new AutoMapperAccount(services);
                //Elastic Search

                services.Configure<CookiePolicyOptions>(options =>
                {
                    //this lambda determines whether use consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.None;
                });
                services.Configure<AccountSetting>(Configuration.GetSection("account"));
                services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddElasticSearch(Configuration);

                //
                services.AddControllers();
                services.AddScoped(p =>
                new MyDB(Configuration["Data:ConnectionString"], Configuration["Data:DbName"]));
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "My API",
                        Description = "ASP.NET Core Web API Sample Example"
                    });

                });
                //
                services.AddTransient<IBookServices, BookServices>();
                services.AddTransient<IAccountServices, AccountSevices>();
                services.AddTransient<ICategoryServices, CategoryServices>();
                services.AddTransient<IElasticSearchAccountService, ElasticSearchAccountService>();
            }
            catch
            {

            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
           {
               c.SwaggerEndpoint("swagger/v1/swagger.json", "Web API");
               c.RoutePrefix = string.Empty;
           });
        }
    }
}
