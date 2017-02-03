using ApplicationCore.DomainServices;
using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationCore.DomainModel;
using System.Collections;
using NI.Data;

namespace Infrastructure.DomainServices
{
    public class ActivitiesRepository : BaseRepository, IActivitiesRepository
    {
        public void SaveActivity(Activity activity)
        {
            if (activity.ID > 0)
            {
                var query = new Query(tableName: "Activities",
                                      condition: new QueryConditionNode((QField)"ID", Conditions.In, new QConst(activity.ID)));

                var fields = new Hashtable() {
                    { "ID", activity.ID },
                    { "ActivityName", activity.Name }
                };

                int result = db.Update(query, fields);
            }
            else
            {
                var query = new Query(tableName: "Activities");

                var fields = new Hashtable() {
                    { "ActivityName", activity.Name }
                };

                db.Insert(tableName: "Activities", data: fields);
            }
        }
        public List<Activity> GetActivities()
        {
            var query = new Query(tableName: "Activities")
            {
                Fields = new[] {
                    (QField)"ID",
                    (QField)"ActivityName"
                }
            };

            IDictionary[] dict = db.LoadAllRecords(query);

            List<Activity> activities = dict.Select(d => new Activity
            {
                ID = Convert.ToInt32(d["ID"]),
                Name = d["ActivityName"].ToString()
            }).ToList();

            return activities;
        }
        public void RemoveActivityByID(int activityId)
        {
            var query = new Query(tableName: "Activities",
                                 condition: (QField)"ID" == new QConst(activityId));

            var result = db.Delete(query);
        }
    }
}
