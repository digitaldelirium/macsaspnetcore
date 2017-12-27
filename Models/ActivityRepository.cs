using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MacsASPNETCore.Models
{
    public class ActivityRepository : IActivityRepository
    {
        private ActivityDbContext _context;

        public ActivityRepository(ActivityDbContext context){
            _context = context;
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.OrderBy( a => a.StartTime).ToList();
        }

        public List<Activity> GetAllActivitiesByMonth(DateTime startDate, DateTime endDate)
        {
            List<Activity> activities = _context.Activities.Where(
                a => a.StartTime >= startDate &&
                a.StartTime <= endDate
                )
            .OrderBy(a => a.StartTime)
            .ToList();
            return activities;
        }

        public List<Calendar> GetAllCalendars()
        {
            return _context.Calendars.OrderBy(c => c.Year).ToList();
        }

        public List<Calendar> GetAllCalendarsWithActivities(){
            return _context.Calendars
            .Include(c => c.Activities)
            .OrderBy(c => c.Year)
            .ToList();
        }


    }
}