using System.Collections.Generic;

namespace MacsASPNETCore.Models
{
    public class Calendar
    {
        public int Id { get; set; }
        public int Year { get; set;}
        public ICollection<Activity> Activities { get; set; }
        
        public Calendar(){
            // Empty Constructor
        }
        
        public Calendar(int year, ICollection<Activity> activityList){
            this.Year = year;
            this.Activities = activityList;
        }
    }
    
}