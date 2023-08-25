using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTimelineGroup2.Models
{
    public class Project
    {
        public  string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set;}

        public int Duration { get; set; }
        public double EstimatedCost { get; set; }

        public Project(string projectCode, string projectName, DateOnly startDate, DateOnly endDate,int duration,double estimatedCost)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            EstimatedCost = estimatedCost;
        }

    }
}
