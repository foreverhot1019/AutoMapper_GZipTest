using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace webApiTest
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
            services.AddAutoMapper(typeof(MyAutoMapperProfile));
            services.AddSingleton(typeof(MyMapprt<,>));

            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(ApiBhvOpts =>
            {
                ApiBhvOpts.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    //foreach (var modelState in actionContext.ModelState.Values)
                    //{
                    //    foreach (var error in modelState.Errors)
                    //    {
                    //        var err = error.ErrorMessage;
                    //    }
                    //}
                    var ArrError = actionContext.ModelState.Values.Select(x => x.AttemptedValue + string.Join(",", x.Errors.Select(n => n.ErrorMessage)));
                    return new JsonResult(new { Success = false, ErrMsg = string.Join(";", ArrError) });
                };
            })
            //开启Gzip等压缩
            //.Configure<BrotliCompressionProviderOptions>(BrOpts =>
            //{
            //    BrOpts.Level = System.IO.Compression.CompressionLevel.Optimal;
            //})
            //.Configure<GzipCompressionProviderOptions>(GzipOpts =>
            //{
            //    GzipOpts.Level = System.IO.Compression.CompressionLevel.Optimal;
            //})
            //.AddResponseCompression(ResOpsts =>
            //{
            //    ResOpsts.EnableForHttps = true;
            //    ResOpsts.Providers.Add<BrotliCompressionProvider>();
            //    ResOpsts.Providers.Add<GzipCompressionProvider>();
            //    ResOpsts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] {
            //        "text/html;charset=utf-8",
            //        "application/xhtml+xml",
            //        "application/atom+xml",
            //        "image/svg+xml"
            //    });
            //})
            ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //开启Gzip等压缩
            //app.UseResponseCompression();

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
