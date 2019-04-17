using System;
using System.ComponentModel.DataAnnotations;

namespace macsaspnetcore.Models
{
    public class Activity
    {
        public int Id { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }
        public string ActivityTitle { get; set; }
        public string ActivityDescription { get; set; }
        public Activity() { }

        public Activity(DateTime startDate, DateTime endDate, string title, string description)
        {
            this.StartTime = startDate;
            this.EndTime = endDate;
            this.ActivityDescription = description;
            this.ActivityTitle = title;
        }
    }
}
