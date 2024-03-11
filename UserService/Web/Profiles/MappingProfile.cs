using AutoMapper;
using Domain.Models;
using Presentation.Models.Request;
using Shared.Models;

namespace Web.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        SetMap_CreateUserRequestToUserDto();
        SetMap_UserDalToUserDto();
        SetMap_UserDtoToUserDal();
    }

    private void SetMap_UserDtoToUserDal()
    {
        CreateMap<UserDto, UserDal>()
            .ForMember(dest => dest.Info, opt =>
                opt.MapFrom(src => new UserInfoDal
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName,
                    About = src.About,
                    Email = src.Email,
                    BirthDate = src.BirthDate
                }));
    }

    private void SetMap_UserDalToUserDto()
    {
        CreateMap<UserDal, UserDto>()
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Info!.Email))
            .ForMember(dest => dest.FirstName, opt =>
                opt.MapFrom(src => src.Info!.FirstName))
            .ForMember(dest => dest.LastName, opt =>
                opt.MapFrom(src => src.Info!.LastName))
            .ForMember(dest => dest.About, opt =>
                opt.MapFrom(src => src.Info!.About))
            .ForMember(dest => dest.BirthDate, opt =>
                opt.MapFrom(src => src.Info!.BirthDate))
            .ForMember(dest => dest.Achievements, opt => opt.Ignore());
    }

    private void SetMap_CreateUserRequestToUserDto()
    {
        CreateMap<CreateUserRequest, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CurrentPoints, opt => opt.Ignore())
            .ForMember(dest => dest.Level, opt => opt.Ignore())
            .ForMember(dest => dest.Achievements, opt => opt.Ignore());
    }
}