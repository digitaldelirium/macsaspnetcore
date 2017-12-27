using System;
using System.Collections.Generic;

namespace MacsASPNETCore.Models
{
    public interface IActivityRepository
    {
        List<Calendar> GetAllCalendars();
        List<Calendar> GetAllCalendarsWithActivities();
        List<Activity> GetAllActivities();
        List<Activity> GetAllActivitiesByMonth(DateTime startTime, DateTime endTime);
    }
}