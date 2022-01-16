using System.Collections.Generic;
using System.Net.Http;
using DowntimeAlerter.Monitoring;
using DowntimeAlerter.Monitoring.Dto;

namespace DowntimeAlerter.Web.ApiClient.Monitoring
{
    public class TargetAppApiClient : ITargetApplicationAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public TargetAppApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<TargetApplicationListDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public TargetApplicationEditDto GetForEdit(int id)
        {
            throw new System.NotImplementedException();
        }

        public void CreateOrEdit(TargetApplicationEditDto editDto)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}