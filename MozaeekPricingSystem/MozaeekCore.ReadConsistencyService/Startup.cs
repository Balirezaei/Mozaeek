using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MozaeekCore.ReadConsistencyService.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MozaeekCore.OutBoxManagement;
using MozaeekCore.Persistense.MongoDb;

namespace MozaeekCore.ReadConsistencyService
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
            services.AddDbContext<EventStoreContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString("EventStoreContext")));
            
            services.Configure<ReadModelDatabaseSettings>(
                Configuration.GetSection(nameof(ReadModelDatabaseSettings)));

            services.AddScoped<IReadModelDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ReadModelDatabaseSettings>>( ).Value);
            
            services.AddScoped<IMongoRepository,MongoRepository>();
            services.AddScoped<ILabelQueryService, LabelQueryService>();
            services.AddScoped<IPointQueryService, PointQueryService>();
            services.AddScoped<IRequestTargetQueryService, RequestTargetQueryService>();
            services.AddScoped<IRequestOrgQueryService, RequestOrgQueryService>();
            services.AddScoped<IRequestActQueryService, RequestActQueryService>();
            services.AddScoped<ISubjectQueryService, SubjectQueryService>();
            services.AddScoped<IRequestQueryService, RequestQueryService>();
            services.AddScoped<IAnnouncementQueryService, AnnouncementQueryService>();
            services.AddScoped<ITechnicianQueryService, TechnicianQueryService>();
            
            services.AddScoped<IOutboxRepository, OutboxRepository>();

            services.ConfigureMassTransit(Configuration);

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
