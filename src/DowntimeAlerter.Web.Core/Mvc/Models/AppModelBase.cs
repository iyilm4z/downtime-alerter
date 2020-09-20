using System.Collections.Generic;
using System.Xml.Serialization;

namespace DowntimeAlerter.Web.Mvc.Models
{
    public class AppModelBase
    {
        public AppModelBase()
        {
            CustomProperties = new Dictionary<string, object>();
        }

        [XmlIgnore]
        public Dictionary<string, object> CustomProperties { get; set; }
    }
}
