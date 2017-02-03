using ApplicationCore.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.DomainServices
{
    public interface IActivitiesRepository : IDisposable
    {
        List<Activity> GetActivities();
        void SaveActivity(Activity activity);
        void RemoveActivityByID(int activityId);
 
    }
}
