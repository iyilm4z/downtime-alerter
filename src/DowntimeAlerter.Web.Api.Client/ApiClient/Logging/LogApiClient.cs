using System.Collections.Generic;
using System.Net.Http;
using DowntimeAlerter.Logging;
using DowntimeAlerter.Logging.Dto;

namespace DowntimeAlerter.Web.ApiClient.Logging
{
    public class LogApiClient : ILogAppService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LogApiClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<LogListDto> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public LogListDto GetForView(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteAll()
        {
            throw new System.NotImplementedException();
        }
    }
}