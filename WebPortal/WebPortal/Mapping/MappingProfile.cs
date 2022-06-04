using AutoMapper;
using WebPortal.db;
using WebPortal.Mapping;

namespace WebPortal.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Link, LinkDto>()
                .ForSourceMember(source => source.Id, opt => opt.DoNotValidate())
                .ForSourceMember(source => source.UserId, opt => opt.DoNotValidate());
                
                
        }
    }
}
