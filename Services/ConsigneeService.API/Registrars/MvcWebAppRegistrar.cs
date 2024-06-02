using Asp.Versioning.ApiExplorer;
using Hangfire;
using ConsigneeService.API;

namespace ConsigneeService.API.Registrars
{
    public class MvcWebAppRegistrar : IWebApplicationRegistrar
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
         //   app.UseRouting();
           

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                }
            });


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Cache-control", "no-store");
                context.Response.Headers.Add("Pragma", "no-cache");

                await next();
            });

            app.UseCookiePolicy(
                  new CookiePolicyOptions
                  {
                      Secure = CookieSecurePolicy.Always
                  }
            );
            
            app.MapGet("/", () => "GetNet Cloud SAAS Consignee Service  - API");
            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();
            app.UseHangfireDashboard("/getnet-consignee-hangfire-dashboard");

            app.MigrateDatabase();
        }
    }
}