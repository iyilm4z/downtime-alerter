using System.Collections.Generic;
using DowntimeAlerter.Logging.Dto;

namespace DowntimeAlerter.Logging
{
    public interface ILogAppService
    {
        List<LogListDto> GetAll();

        LogListDto GetForView(int id);

        void Delete(int id);

        void DeleteAll();
    }
}
