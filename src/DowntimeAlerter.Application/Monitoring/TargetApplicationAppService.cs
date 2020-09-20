using System.Collections.Generic;
using System.Linq;
using DowntimeAlerter.Application.Services;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Mapper;
using DowntimeAlerter.Monitoring.Dto;

namespace DowntimeAlerter.Monitoring
{
    public class TargetApplicationAppService : ApplicationService, ITargetApplicationAppService
    {
        private readonly IRepository<TargetApplication> _targetApplicationRepository;

        public TargetApplicationAppService(IRepository<TargetApplication> targetApplicationRepository)
        {
            _targetApplicationRepository = targetApplicationRepository;
        }

        public List<TargetApplicationListDto> GetAll()
        {
            var entities = _targetApplicationRepository.GetAllList();

            return entities.Select(entity => entity.ToModel<TargetApplicationListDto>()).ToList();
        }

        public TargetApplicationEditDto GetForEdit(int id)
        {
            var entity = _targetApplicationRepository.Get(id);

            return entity.ToModel<TargetApplicationEditDto>();
        }

        public void CreateOrEdit(TargetApplicationEditDto editDto)
        {
            if (editDto.Id <= 0)
            {
                var entity = editDto.ToEntity<TargetApplication>();
                _targetApplicationRepository.Insert(entity);
            }
            else
            {
                var entity = _targetApplicationRepository.Get(editDto.Id);
                editDto.ToEntity(entity);
                _targetApplicationRepository.Update(entity);
            }
        }

        public void Delete(int id)
        {
            _targetApplicationRepository.Delete(id);
        }
    }
}