using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebApi.Bll.Dtos;
using AutoMapper;
using WebApi.DAL;

namespace WebApi.API
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<VelhoContext>(o =>
                o.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"])
                .ConfigureWarnings(
                    c => c.Throw(RelationalEventId.QueryClientEvaluationWarning)));


#pragma warning disable CS0618 // Type or member is obsolete
            services.AddAutoMapper(cfg =>
            {
                cfg.CreateMap<Entities.Person, Person>()
                .AfterMap((p, dto, ctx) =>
                    dto.Meetings = p.PersonMeetings.Select(pm =>
                        ctx.Mapper.Map<Meeting>(pm.Meeting)).ToList()).ReverseMap();
                cfg.CreateMap<Entities.Meeting, Meeting>().ReverseMap();
                cfg.CreateMap<Entities.Note, Note>().ReverseMap();
            });
#pragma warning restore CS0618 // Type or member is obsolete
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
