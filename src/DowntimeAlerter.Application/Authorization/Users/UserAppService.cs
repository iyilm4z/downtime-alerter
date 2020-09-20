using System;
using System.Collections.Generic;
using System.Linq;
using DowntimeAlerter.Application.Services;
using DowntimeAlerter.Authorization.Users.Dto;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Mapper;

namespace DowntimeAlerter.Authorization.Users
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IRepository<User> _userRepository;

        public UserAppService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserListDto> GetAll()
        {
            var entities = _userRepository.GetAllList();

            return entities.Select(entity => entity.ToModel<UserListDto>()).ToList();
        }

        public UserEditDto GetForEdit(int id)
        {
            var entity = _userRepository.Get(id);

            return entity.ToModel<UserEditDto>();
        }

        public void CreateOrEdit(UserEditDto editDto)
        {
            if (editDto.Id <= 0)
            {
                var entity = editDto.ToEntity<User>();
                _userRepository.Insert(entity);
            }
            else
            {
                var entity = _userRepository.Get(editDto.Id);
                if (entity.Role == UserRole.Admin)
                {
                    entity.UserName = AppDefaults.AdminUserName;
                }

                editDto.ToEntity(entity);
                _userRepository.Update(entity);
            }
        }

        public void Delete(int id)
        {
            var entity = _userRepository.FirstOrDefault(id);
            if (entity != null && entity.Role == UserRole.Admin)
            {
                throw new Exception($"Admin can not be deleted.");
            }

            _userRepository.Delete(id);
        }
    }
}