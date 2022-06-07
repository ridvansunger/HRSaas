using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using HRSaas.Business.Abstract;
using HRSaas.Business.Concrete;
using HRSaas.Business.Mapping;
using HRSaas.Business.ValidationRules;
using HRSaas.DAL.Abstract;
using HRSaas.DAL.Concrete;
using HRSaas.DAL.Context;
using HRSaas.Entities.Concrete;
using HRSaas.Entities.Dtos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace HRSaas.UI
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
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile<GeneralProfile>());
            var mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddRazorPages();
            //services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PersonalValidator>());
            services.AddFluentValidation();
            services.AddTransient<IValidator<PersonalDetailVm>, PersonalValidator>();
            services.AddTransient<IValidator<LoginVM>, LoginVMValidator>();
            services.AddTransient<IValidator<PersonalExpenditureVM>, PersonalExpenditureValidator>();
            services.AddTransient<IValidator<LeaveVM>, LeaveValidator>();
            services.AddTransient<IValidator<EmployeeUpdateVM>, EmployeeUpdateVMValidator>();
            services.AddTransient<IValidator<AdvanceVM>, AdvanceVMValidator>();
            services.AddTransient<IValidator<PackageVM>, PackageValidator>();
            services.AddTransient<IValidator<CompanyVM>, CompanyVMValidator>();

            services.AddControllersWithViews();
            services.AddDbContext<HRSaasContext>(t0 => t0.UseSqlServer(Configuration.GetConnectionString("HRSaasDB")));
            services.AddIdentity<Personal, AppRole>(options=> { options.User.RequireUniqueEmail = true;options.Password.RequireDigit = false;options.Password.RequireUppercase = false;options.Password.RequiredLength = 1;options.Password.RequiredUniqueChars = 0;options.Password.RequireNonAlphanumeric = false;options.Password.RequireLowercase = false; }).AddDefaultTokenProviders().AddEntityFrameworkStores<HRSaasContext>();


            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IPersonalRepository, PersonalRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICountyRepository, CountyRepository>();
            services.AddScoped<ITitleRepository, TitleRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IAdvanceRepository, AdvanceRepository>();
            services.AddScoped<ILeaveRepository, LeaveRepository>();
            services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
            services.AddScoped<IExpenditureRepository, ExpenditureRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<IPackageBusiness, PackageBusiness>();
            services.AddScoped<IPersonalBusiness, PersonalBusiness>();
            services.AddScoped<IDepartmentBusiness, DepartmentBusiness>();
            services.AddScoped<ITitleBusiness, TitleBusiness>();
            services.AddScoped<ICityBusiness, CityBusiness>();
            services.AddScoped<ICountyBusiness, CountyBusiness>();
            services.AddScoped<IAddressBusiness, AddressBusiness>();
            services.AddScoped<IAdvanceBusiness, AdvanceBusiness>();
            services.AddScoped<ILeaveBusiness, LeaveBusiness>();
            services.AddScoped<ILeaveTypeBusiness, LeaveTypeBusiness>();
            services.AddScoped<IExpenditureBusiness, ExpenditureBusiness>();
            services.AddScoped<ICompanyBusiness, CompanyBusiness>();


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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();


            app.UseRouting();
            //app.UseMvc();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapAreaControllerRoute(
                  name: "Employee",
                    areaName: "Employee",
                  pattern: "Employee/{controller=Home}/{action=Index}/{id?}");
              
                endpoints.MapAreaControllerRoute(
                  name: "SiteManager",
                    areaName: "SiteManager",
                  pattern: "SiteManager/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                  name: "Manager",
                    areaName: "Manager",
                  pattern: "Manager/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
