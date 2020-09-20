using AutoMapper;
using DowntimeAlerter.Authorization.Users;
using DowntimeAlerter.Authorization.Users.Dto;
using DowntimeAlerter.Logging;
using DowntimeAlerter.Logging.Dto;
using DowntimeAlerter.Mapper;
using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Monitoring.Dto;

namespace DowntimeAlerter
{
    public class ApplicationMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public ApplicationMapperConfiguration()
        {
            CreateUserMaps();
            CreateTargetApplicationMaps();
            CreateLogMaps();
        }

        private void CreateUserMaps()
        {
            CreateMap<User, UserListDto>();
            CreateMap<User, UserEditDto>().ReverseMap();
        }

        private void CreateTargetApplicationMaps()
        {
            CreateMap<TargetApplication, TargetApplicationListDto>();
            CreateMap<TargetApplication, TargetApplicationEditDto>().ReverseMap();
        }

        private void CreateLogMaps()
        {
            CreateMap<Log, LogListDto>();
        }

        public int Order => 0;
    }
}