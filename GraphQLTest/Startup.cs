using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLTest.GQLQueries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLTest
{
    public class Startup
    {
        //http://fiyazhasan.me/graphql-with-asp-net-core/
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var schema = new Schema { Query = new HelloWorldQuery() };

            app.Run(async (context) =>
            {
                var result = await new DocumentExecuter().ExecuteAsync(doc =>
                {
                    doc.Schema = schema;
                    doc.Query = @"query { hello
                                          howdy 
                                        }";
                })
                .ConfigureAwait(false);

                var json = new DocumentWriter(indent: true).Write(result);
                await context.Response.WriteAsync(json);
            });
        }
    }
}
