using _swagger.Models;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace _swagger.Mapper
{
    public class AutoMapperAccount : Profile
    {
        public IMapper _mapper;
        public AutoMapperAccount(IServiceCollection services)
        {
            try
            {
                CreateMap<AccountViewModel, Account>();
                var mapperConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(this);
                });
                IMapper mapper = mapperConfig.CreateMapper();
                services.AddSingleton(mapper);
                setupMapper();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void setupMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountViewModel, Account>();
            });
            _mapper = config.CreateMapper();
        }
    }
}
