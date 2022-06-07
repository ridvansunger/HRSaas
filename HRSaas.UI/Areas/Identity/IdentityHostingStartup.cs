
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(HRSaas.UI.Areas.Identity.IdentityHostingStartup))]
//namespace HRSaas.UI.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            //builder.ConfigureServices((context, services) => {
//            //    services.AddDbContext<HRSaasUIContext>(options =>
//            //        options.UseSqlServer(
//            //            context.Configuration.GetConnectionString("HRSaasUIContextConnection")));

//            //    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//            //        .AddEntityFrameworkStores<HRSaasUIContext>();
//            //});
//        }
//    }
//}