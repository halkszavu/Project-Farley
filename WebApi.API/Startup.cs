using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using WebApi.Bll.Dtos;
using WebApi.Bll.Services;
using WebApi.DAL;

namespace WebApi.API
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).
                AddJsonOptions(
                    json => json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<VelhoContext>(o =>
                o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                .ConfigureWarnings(
                    c => c.Throw(RelationalEventId.QueryClientEvaluationWarning)));

            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IMeetingService, MeetingService>();

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Entities.Person, Person>()
                .AfterMap((p, dto, ctx) =>
                    dto.Meetings = p.PersonMeetings.Select(pm =>
                        ctx.Mapper.Map<Meeting>(pm.Meeting)).ToList()).ReverseMap();
                cfg.CreateMap<Entities.Meeting, Meeting>()
                .AfterMap((m, dto, ctx) =>
                    dto.People = m.PersonMeetings.Select(pm =>
                      ctx.Mapper.Map<Person>(pm.Person)).ToList()).ReverseMap();
                cfg.CreateMap<Entities.Person, Person>().ReverseMap();
                cfg.CreateMap<Entities.Note, Note>().ReverseMap();
            });
#pragma warning restore CS0618 // Type or member is obsolete

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseProblemDetails();
            app.UseMvc();
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member