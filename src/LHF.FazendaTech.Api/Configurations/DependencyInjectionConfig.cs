using LHF.FazendaTech.Api.Extensions.IdentityUser;
using LHF.FazendaTech.Business.Intefaces;
using LHF.FazendaTech.Business.Intefaces.Base;
using LHF.FazendaTech.Business.Notificacoes;
using LHF.FazendaTech.Business.Services;
using LHF.FazendaTech.Data.Context;
using LHF.FazendaTech.Data.Repository;
using LHF.FazendaTech.Data.UoW;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace LHF.FazendaTech.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            // Data
            services.AddScoped<FazendaTechContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IFazendaRepository, FazendaRepository>();

            // Business
            services.AddScoped<INotificador, Notificador>();
            services.AddScoped<IFazendaService, FazendaService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAppIdentityUser, AppIdentityUser>();

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }
    }
}
