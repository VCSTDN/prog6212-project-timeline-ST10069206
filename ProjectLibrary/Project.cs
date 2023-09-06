using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

//This is done if you have created a WPF first. 
//copy namespace from the Class1 that VS created, then delete Class1. 
namespace ProjectLibrary
{
    public class Project
    {
        public string? ProjectCode { get; set; }
        public string? ProjectName { get; set; }

        public string name
        {
            get
            {
                return name;
            }
            set
            {
                if (name.Length < 3)
                {
                    throw new Exception("The project name should not be less than 3. ")
                }
            }
        }

        private DateTime startDate;
        public DateTime StartDate 
        {
            get 
            { 
                return startDate; 
            }
            set 
            { 
                if (startDate>EndDate)
                {
                    throw new Exception($"Start date {startDate} should not be after end date.");
                }
                else
                {
                    startDate = value; 
                }
                
            }
        }
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
            return (hourlyRate * 8) *Duration;
        }

        public override string ToString()
        {
            return $"({ProjectCode}) {ProjectName} - {Duration}, Estimated Cost: R{EstimatedCost.ToString("C2")}";
        }

        /// <summary>
        /// Indexer
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
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
