using AutoMapper;
using BookApi.Models.Account;

namespace BookApi.Models.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            try
            {
                //Add more field have same, so won't have fullname and confirm password
                //CreateMap<UserViewModel, AppUser>();    
                CreateMap<UserViewModel, AppUser>().ForMember(dest => dest.fullName, options => options.MapFrom(source => source.DisplayName))
                                                  .ForMember(dest => dest.fullName, options => options.Condition(source => source.DisplayName != source.userName));
            }
            catch
            {

            }
        }
    }
}
