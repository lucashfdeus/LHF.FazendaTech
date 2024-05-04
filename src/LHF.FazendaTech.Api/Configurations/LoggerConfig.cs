using LHF.FazendaTech.Api.Extensions;

namespace LHF.FazendaTech.Api.Configurations
{
    public static class LoggerConfig
    {

        public static IServiceCollection AddLoggingConfig(this IServiceCollection services, IConfiguration configuration)
        {
            //Configurar novas apikey no elmah.
            services.AddElmahIo(o =>
            {
                o.ApiKey = "72653c04b6ad41db826c2c6008afda48";
                o.LogId = new Guid("fe72cff6-9bee-4434-a2b4-30716e61f719");
            });

            services.AddHealthChecks()
                .AddElmahIoPublisher(options =>
                {
                    options.ApiKey = "72653c04b6ad41db826c2c6008afda48";
                    options.LogId = new Guid("fe72cff6-9bee-4434-a2b4-30716e61f719");
                    options.HeartbeatId = "API FazendaTech";

                })
                .AddCheck("Fazendas", new SqlServerHealthCheck(configuration.GetConnectionString("DefaultConnection")))
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"), name: "FazendaTech");

            services.AddHealthChecksUI()
                .AddSqlServerStorage(configuration.GetConnectionString("DefaultConnection"));

            //Teste configuração Elmah
            //services.AddLogging(builder =>
            //{
            //    builder.AddElmahIo(o =>
            //    {
            //        o.ApiKey = "72653c04b6ad41db826c2c6008afda48";
            //        o.LogId = new Guid("7a7cfcb7-4fe5-4ac0-a741-6cf3f0abffa3");
            //    });
            //    builder.AddFilter<ElmahIoLoggerProvider>(null, LogLevel.Warning);
            //});

            return services;
        }

        public static IApplicationBuilder UseLoggingConfiguration(this IApplicationBuilder app)
        {
            app.UseElmahIo();

            return app;
        }
    }
}
