using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotChocolate;
using HotChocolate.AspNetCore.GraphiQL;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Subscriptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using MRobot.Api.Types;

namespace MRobot.Api
{
  public class Startup
  {
    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddSingleton<Query>();

      // Add in-memory event provider
      var eventRegistry = new InMemoryEventRegistry();
      services.AddSingleton<IEventRegistry>(eventRegistry);
      services.AddSingleton<IEventSender>(eventRegistry);

      // Add GraphQL Services
      services.AddGraphQL(sp => Schema.Create(c =>
      {
        c.RegisterServiceProvider(sp);

        // Adds the authorize directive and
        // enables the authorization middleware.
        c.RegisterAuthorizeDirectiveType();

        c.RegisterQueryType<QueryType>();

        c.RegisterType<SettingsType>();
      }));

      // Add Authorization Policies
      // services.AddAuthorization(options =>
      // {
      //   options.AddPolicy("HasCountry", policy =>
      //             policy.RequireAssertion(context =>
      //                 context.User.HasClaim(c =>
      //                     (c.Type == ClaimTypes.Country))));
      // });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseWebSockets();
      app.UseGraphQL();
      app.UseGraphiQL();
      app.UsePlayground();
    }
  }
}
