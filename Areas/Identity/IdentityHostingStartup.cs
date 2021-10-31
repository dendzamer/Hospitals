using System;
using Hospitals.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Hospitals.Areas.Identity.IdentityHostingStartup))]
namespace Hospitals.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HospitalsIdentityDbContext>(options =>
                    options.UseSqlite(
                        context.Configuration.GetConnectionString("HospitalsIdentityDbContextConnection")));
            });
        }
    }
}
