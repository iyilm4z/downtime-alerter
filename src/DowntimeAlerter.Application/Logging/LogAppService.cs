using System.Collections.Generic;
using System.Linq;
using DowntimeAlerter.Domain.Repositories;
using DowntimeAlerter.Logging.Dto;
using DowntimeAlerter.Mapper;

namespace DowntimeAlerter.Logging
{
    public class LogAppService : ILogAppService
    {
        private readonly IRepository<Log> _logRepository;

        public LogAppService(IRepository<Log> logRepository)
        {
            _logRepository = logRepository;
        }

        public List<LogListDto> GetAll()
        {
            var entities = _logRepository.GetAllList();

            return entities.Select(entity => entity.ToModel<LogListDto>()).ToList();
        }

        public LogListDto GetForView(int id)
        {
            var entity = _logRepository.Get(id);

            return entity.ToModel<LogListDto>();
        }

        public void Delete(int id)
        {
            _logRepository.Delete(id);
        }

        public void DeleteAll()
        {
            var entities = _logRepository.GetAllList();
            foreach (var entity in entities)
            {
                _logRepository.Delete(entity);
            }
        }
    }
}
