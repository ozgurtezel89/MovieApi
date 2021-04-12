using Microsoft.AspNetCore.Builder;

namespace Movie.Api.Configurations
{
    public static class StartupConfigurations
    {
        ///<summary>Adds Swagger to the API</summary>
        public static IApplicationBuilder AddSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie API V1");
            });

            return app;
        }
    }
}
