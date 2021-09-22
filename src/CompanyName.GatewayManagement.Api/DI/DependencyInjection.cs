using Microsoft.Extensions.DependencyInjection;
using CompanyName.GatewayManagement.Api.Presenter;
using CompanyName.GatewayManagement.Data;
using CompanyName.GatewayManagement.Domain.Interfaces;
using CompanyName.GatewayManagement.Domain.Services;

namespace CompanyName.GatewayManagement.Api.DI
{
    public class DependencyInjection
    {
        private readonly IServiceCollection _services;

        public DependencyInjection(IServiceCollection services)
        {
            _services = services;
        }

        public void InjectAll()
        {
            _services.AddSingleton<IUnitOfWork, UnitOfWork>();
            _services.AddScoped<IGatewayService, GatewayService>();
            _services.AddScoped<IDeviceService, DeviceService>();
            _services.AddScoped<IResponseWrappable, WrapperedResponse>();
        }
    }
}
