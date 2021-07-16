using System;

namespace alison_etl_api.Models
{
    public class Pipeline
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ScheduledMinutes { get; set; }
        public DateTime LastStart { get; set; }
        public DateTime LastCompleted { get; set; }
        public string Status { get; set; }
    }
}