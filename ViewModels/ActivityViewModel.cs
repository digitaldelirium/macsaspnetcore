using System;
using System.ComponentModel.DataAnnotations;

namespace macsaspnetcore.ViewModels
{
    public class ActivityViewModel
    {
        [Required]
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        public string EventTitle { get; set; }
        public string EventDescription { get; set; }

        public ActivityViewModel() { }

        public ActivityViewModel(DateTime eventTime, string eventTitle, string eventDescription)
        {
            this.StartTime = eventTime;
            this.EventTitle = eventTitle;
            this.EventDescription = eventDescription;
        }
    }
}