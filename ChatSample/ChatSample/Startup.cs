using ChatSample.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChatSample
{
    public class Startup
    {
        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();

            //  添加CORS，使得任何主机即可访问
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.AllowAnyMethod()
                                             .SetIsOriginAllowed(_ => true)
                                              .AllowAnyHeader()
                                              .AllowCredentials();
                                  });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //  使用CORS
            app.UseCors(MyAllowSpecificOrigins);

            app.UseFileServer();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //  服务端地址: http:Ip/receivemessagehub
                endpoints.MapHub<ChatHub>("/receivemessagehub");
            });
        }
    }
}
