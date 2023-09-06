using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//This is done if you have created a WPF first. 
//copy namespace from the Class1 that VS created, then delete Class1. 
namespace ProjectLibrary
{
    public class Project
    {
        public string ProjectCode { get; set; }
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Duration { get; set; }
        public double EstimatedCost { get; set; }
        public List<Project> projectList { get; set; }

        public Project(string projectCode, string projectName, DateTime startDate, DateTime endDate)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            Duration = (endDate.Date - startDate.Date).Days;
        }
        public Project() { }

        public double CalcEstimatedCost(double hourlyRate)
        {
            EstimatedCost = hourlyRate * 8;
            return EstimatedCost;
        }

        public override string ToString()
        {
            return $"({ProjectCode}) {ProjectName} - {Duration}, EC:R{EstimatedCost}";
        }

        public Project this[string code]
        {
            get
            {
                Project pr = new Project();
                foreach (Project proj in projectList)
                {
                    if (proj.ProjectCode.Equals(code))
                    {
                        pr = proj;
                    }
                }
                return pr;
            }
        }
    }
}
