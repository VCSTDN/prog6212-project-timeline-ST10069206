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
        private string? projectName;

        public string ProjectName
        {
            get
            {
                return projectName;
            }
            set
            {
                if (ProjectName.Length < 3)
                {
                    throw new Exception("The project name should not be less than 3. ");
                }
                else
                {
                    ProjectName = value;
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
        public List<Project> ProjectList { get; set; }

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
                foreach (Project proj in ProjectList)
                {
                    if (proj.ProjectCode.Equals(code))
                    {
                        pr = proj;
                    }
                }
                return pr;
            }
        }

        /// <summary>
        /// Method that returns a list of projects between a 2 dates. 
        /// </summary>
        /// <param name="sDate"></param>
        /// <param name="eDate"></param>
        /// <returns></returns>
        public List<Project> GetProjectsBetween(DateTime sDate, DateTime eDate)
        {
            List<Project> projectsBetween = new List<Project>();
            foreach(Project proj in ProjectList) 
            {
                if (proj.StartDate>=sDate && proj.StartDate<=eDate)
                {
                    projectsBetween.Add(proj);
                }
            }
            return projectsBetween;
        }

        /// <summary>
        /// Method that returns a list of projects which has a duration longer than 6 weeks. 
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProjectsDuration()
        {
            List<Project> projectsDuration = new List<Project>();
            foreach (Project proj in ProjectList)
            {
                if ((proj.Duration/5)>6)
                {
                    projectsDuration.Add(proj);
                }
            }
            return projectsDuration;
        }

        /// <summary>
        /// Method that returns a list of projects which have ended. 
        /// </summary>
        /// <returns></returns>
        public List<Project> GetProjectsEnded()
        {
            List<Project> projectsEnded = new List<Project>();
            foreach (Project proj in ProjectList)
            {
                if (proj.EndDate<DateTime.Today)
                {
                    projectsEnded.Add(proj);
                }
            }
            return projectsEnded;
        }

        /// <summary>
        /// Method that returns a list of projects which were started in a specific month. 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<Project> GetProjectsMonth(int month) 
        {
            List<Project> projectsMonth = new List<Project>();
            foreach (Project proj in ProjectList)
            {
                if (proj.StartDate.Month.Equals(month))
                {
                    projectsMonth.Add(proj);
                }
            }
            return projectsMonth;
        }
    }
}
