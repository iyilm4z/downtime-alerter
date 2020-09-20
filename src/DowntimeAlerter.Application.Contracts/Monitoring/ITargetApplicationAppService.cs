using System.Collections.Generic;
using DowntimeAlerter.Application.Services;
using DowntimeAlerter.Monitoring.Dto;

namespace DowntimeAlerter.Monitoring
{
    public interface ITargetApplicationAppService : IApplicationService
    {
        List<TargetApplicationListDto> GetAll();
        TargetApplicationEditDto GetForEdit(int id);
        void CreateOrEdit(TargetApplicationEditDto editDto);
        void Delete(int id);
    }
}
