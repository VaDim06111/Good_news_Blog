﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
using CQS_MediatR.Commands.CommandEntities;
using Good_news_Blog.Data;
using Good_news_Blog.EmailService;
using Good_news_Blog.WebAPI.Filters;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ParseNewsFromTutByUsingCQS;
using ParserNewsFromOnlinerUsingCQS;
using ParserNewsFromS13UsingCQS;


namespace Good_news_Blog.WebAPI
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
            //===== Add services to Database =====
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDbContext>(options
                => options.UseSqlServer(connection));
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //===== Add services to Parse news from web =====
            services.AddTransient<INewsOnlinerParser, NewsParserFromOnliner>();
            services.AddTransient<INewsS13Parser, NewsParserFromS13>();
            services.AddTransient<INewsTutByParser, NewsParserFromTutBy>();

            //===== Add EmailService =====
            services.AddTransient<IEmailSender, SmtpEmailService>();

            //===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JWT:JwtIssuer"],
                        ValidAudience = Configuration["JWT:JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddControllersAsServices();

            //===== Add MediatR =====
            var assembly = AppDomain.CurrentDomain.Load("CQS_MediatR");
            services.AddMediatR(assembly);
            services.AddTransient<IMediator, Mediator>();

            services
                .AddHangfire(config
                    => config.UseSqlServerStorage(
                        Configuration
                            .GetConnectionString("DefaultConnection")));

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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseStatusCodePages();

            app.UseMvc();

            app.UseHangfireServer();
            app.UseHangfireDashboard("/api/admin/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            RecurringJob.AddOrUpdate(
                () => GetNews(app.ApplicationServices.GetService<INewsOnlinerParser>(),
                    app.ApplicationServices.GetService<INewsS13Parser>(),
                    app.ApplicationServices.GetService<INewsTutByParser>(),
                    app.ApplicationServices.GetService<IMediator>()),
                Cron.Minutely);

        }
        public async Task GetNews(INewsOnlinerParser parserFromOnliner, INewsS13Parser newsParserFromS13, INewsTutByParser newsParserFromTutBy, IMediator mediator)
        {
            IEnumerable<News> newsS13 = new List<News>();
            IEnumerable<News> newsOnliner = new List<News>();
            IEnumerable<News> newsTutBy = new List<News>();

            Parallel.Invoke(
                () =>
                {
                    newsS13 = newsParserFromS13.GetFromUrl();
                },
                () =>
                {
                    newsOnliner = parserFromOnliner.GetFromUrl();
                },
                () =>
                {
                    newsTutBy = newsParserFromTutBy.GetFromUrl();
                });

            await mediator.Send(new AddRangeNewsCommand(newsS13));
            await mediator.Send(new AddRangeNewsCommand(newsOnliner));
            await mediator.Send(new AddRangeNewsCommand(newsTutBy));
        }
    }
}
