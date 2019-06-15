using AutoMapper;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using WebApi.Bll.Dtos;
using WebApi.Bll.Exceptions;
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

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
            });
            
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<INoteService, NoteService>();
            services.AddTransient<IMeetingService, MeetingService>();

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Entities.Person, PersonDto>()
                .AfterMap((p, dto, ctx) =>
                    dto.Meetings = p.PersonMeetings.Select(pm =>
                        ctx.Mapper.Map<MeetingDto>(pm.Meeting)).ToList()).ReverseMap();
                cfg.CreateMap<Entities.Meeting, MeetingDto>()
                .AfterMap((m, dto, ctx) =>
                    dto.People = m.PersonMeetings.Select(pm =>
                      ctx.Mapper.Map<PersonDto>(pm.Person)).ToList()).ReverseMap();
                cfg.CreateMap<Entities.Person, PersonDto>().ReverseMap();
                cfg.CreateMap<Entities.Note, NoteDto>().ReverseMap();
            });
#pragma warning restore CS0618 // Type or member is obsolete

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(2, 0);
                o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddProblemDetails(options =>
            {
                options.Map<EntityNotFoundException>(ex =>
                new ProblemDetails
                {
                    Title = "Entity Not Found",
                    Status = StatusCodes.Status404NotFound,
                });
                options.Map<DuplicateEntityException>(ex =>
                new ProblemDetails
                {
                    Title = "Duplicate of the same entity found",
                    Status = StatusCodes.Status405MethodNotAllowed,
                });
                options.Map<DbUpdateConcurrencyException>(ex=> new ConcurrencyProblemDetails(ex));
            });

            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseProblemDetails();
            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member