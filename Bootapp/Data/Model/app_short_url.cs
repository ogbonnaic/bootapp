using System;
namespace Bootapp.Data.Model
{
    public class app_short_url
    {
        public Guid id { set; get; }
        public string short_url { get; set; }
        public string original_url { set; get; }
        public DateTime created_at { set; get; }
    }
}
