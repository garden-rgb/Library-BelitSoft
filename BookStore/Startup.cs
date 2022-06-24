using AutoMapper;
using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Mapper;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Services;
using BookLibrary.DAL.DatabaseSecret;
using BookLibrary.DAL.Interfaces;
using BookLibrary.DAL.Repositories;
using BookLibrary.Web.Mapper;
using Hangfire;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace BookLibrary.Web
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
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<StoreContext>(options => options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IMessageService, MessageService>();

            services.AddSingleton<ShoppingCart>();

            services.AddControllersWithViews();

            services.AddHangfire(configuration => configuration.UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
                mc.AddProfile(new MapperProfileData());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.AddSingleton<IEmailService, EmailService>();

            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IRecurringJobManager backgroundJobs, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseOwin();
            
            app.UseHangfireDashboard();
            backgroundJobs.AddOrUpdate<IMessageService>("sendmessagewarning", x => x.SendMessageWarning(), Cron.Daily);
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHangfireDashboard();
            });
        }
    }
}
