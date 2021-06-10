using AutoMapper;
using BookApi.Models.DatabaseSettings;
using BookApi.Models.Mapper;
using BookApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BookApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void mapp_AU_UVM(IServiceCollection services)
        {
            //Map UserViewModel class to AppUser class
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Mapper
            mapp_AU_UVM(services);
            //Book
            services.Configure<BookstoreDatabaseSettings>(Configuration.GetSection(nameof(BookstoreDatabaseSettings)));
            services.AddSingleton<IBookstoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);
            services.AddSingleton<BooksServices>();
            //Category
            services.Configure<CategoryBookDatabaseSettings>(Configuration.GetSection(nameof(CategoryBookDatabaseSettings)));
            services.AddSingleton<ICategoryBookDatabaseSettings>(sp => sp.GetRequiredService<IOptions<CategoryBookDatabaseSettings>>().Value);
            services.AddSingleton<CategoryServices>();
            //User
            services.Configure<AppUserDatabaseSettings>(Configuration.GetSection(nameof(AppUserDatabaseSettings)));
            services.AddSingleton<IAppUserDatabaseSettings>(sp => sp.GetRequiredService<IOptions<AppUserDatabaseSettings>>().Value);
            services.AddSingleton<UsersServices>();
            //
            services.AddControllers();
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
        }
    }
}
