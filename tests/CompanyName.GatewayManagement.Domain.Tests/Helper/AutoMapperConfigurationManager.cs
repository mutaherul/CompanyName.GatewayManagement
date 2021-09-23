using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace CompanyName.GatewayManagement.Domain.Tests.Helper
{
    public static class AutoMapperConfigurationManager
    {
        private static IMapper _mapper;

        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    var mapperConfiguration = new MapperConfiguration(configExp =>
                    {
                        var assembly = Assembly.Load("CompanyName.GatewayManagement.Domain");
                        var types = assembly.GetTypes();
                        var profiles = types.Where(it => it.IsSubclassOf(typeof(Profile))).ToList();
                        profiles.ForEach(obj => configExp.AddProfile((Profile)Activator.CreateInstance(obj)));
                    });
                    _mapper = mapperConfiguration.CreateMapper();
                }
                return _mapper;
            }
        }
    }
}
