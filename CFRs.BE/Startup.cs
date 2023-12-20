using CFRs.BE.Batch;
using Hangfire;
using Microsoft.AspNetCore.Builder;

namespace CFRs.BE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        [Obsolete]
        public void Configure(IApplicationBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

            var thai_timezone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");

            //"0 5 * * *"   ตีห้า
            //*/5 * * * *   ทุกๆ 5 นาที

            RecurringJob.AddOrUpdate<Batch_EBAO_LS>(a => a.Get(), "0 5 * * *", thai_timezone);

            RecurringJob.AddOrUpdate<Batch_EBAO_GLS>(a => a.Get(), "0 5 * * *", thai_timezone);

            RecurringJob.AddOrUpdate<Batch_DMS>(a => a.Get(), "0 5 * * *", thai_timezone);

            RecurringJob.AddOrUpdate<Batch_SMART_RP>(a => a.Get(), "0 5 * * *", thai_timezone);

            RecurringJob.AddOrUpdate<Batch_SAP>(a => a.Get(), "0 5 * * *", thai_timezone);

            RecurringJob.AddOrUpdate<Batch_Group_Kru>(a => a.Get(), "0 5 * * *", thai_timezone);
        }

        public void ConfigConfigureServies(IServiceCollection services)
        {
            //services.AddHangfire(config => {
            //    //config.UseSqlServerStorage("Server=tcp:localhost,1433;Database=HangfireDemo;User Id=sa;Password=p@ssw0rd;");
            //    config.UseSqlServerStorage(CFRs.DAL.Helper.GetSettingsHelper.CFRsConnectionString);
            //});

            //services.AddHangfireServer();
        }
    }
}