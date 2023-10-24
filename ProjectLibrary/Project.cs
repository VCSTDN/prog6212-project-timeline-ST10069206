using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

//This is done if you have created a WPF first. 
//copy namespace from the Class1 that VS created, then delete Class1. 
namespace ProjectLibrary
{
    public class Project
    {
        public string? ProjectCode { get; set; }
        private string? ProjectName;

        public string projectName
        {
            get
            {
                return ProjectName;
            }
            set
            {
                if (value.Trim().Length < 3)
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
        /// <summary>
        /// Delegate which points to DisplayNotifiction method. 
        /// </summary>
        /// <returns></returns>
        public delegate string ProjectFiveDays();

        /// <summary>
        /// Display notifiction method
        /// </summary>
        /// <returns></returns>
        public string DisplayNotification()
        {
            return "There is 5 days left before the projects end date.";   
        }
        /// <summary>
        /// Event which notifys that the project has 5 days left before the end date. 
        /// </summary>
        public event ProjectFiveDays OnFiveDays;
        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                if ((endDate - DateTime.Today).Days.Equals(5))
                {
                    
                    //OnFiveDays.Invoke((this.EndDate-DateTime.Today).Days, 5);
                }
                endDate = value;
            }
        }
        public int Duration { get; set; }
        public double EstimatedCost { get; set; }

        public static List<Project> ProjectList=new List<Project>();
        //public static List<Project> ProjectList = new List<Project>()
        //{
        //    new("P100", "Sony", Convert.ToDateTime("06-05-2023"),Convert.ToDateTime("16-06-2023"),150 ),
        //    new("P101", "xbox", Convert.ToDateTime("09-10-2023"),Convert.ToDateTime("16-10-2023"),230 ),
        //    new("P102", "Microsoft", Convert.ToDateTime("15-01-2023"),Convert.ToDateTime("19-03-2023"),180 ),
        //    new("P103", "Nokia", Convert.ToDateTime("28-08-2023"),Convert.ToDateTime("26-09-2023"),190 ),
        //    new("P104", "Panasonic", Convert.ToDateTime("07-02-2023"),Convert.ToDateTime("11-04-2023"),200 ),
        //    new("P105", "Anglo", Convert.ToDateTime("02-12-2023"),Convert.ToDateTime("19-01-2024"),140 ),
        //};

        public Project(string projectCode, string projectName, DateTime startDate, DateTime endDate, double rate)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            //Duration = (endDate.Date - startDate.Date).Days;
            Duration = GetDuration(StartDate, EndDate);
            EstimatedCost=CalcEstimatedCost(rate);
        }
        public Project() { }

        public Project(string projectCode, string projectName, DateTime startDate, DateTime endDate, int duration, double estimatedCost)
        {
            ProjectCode = projectCode;
            ProjectName = projectName;
            StartDate = startDate;
            EndDate = endDate;
            Duration = duration;
            EstimatedCost = estimatedCost;
        }

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
        public  Project this[string code]
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

        public List<Project> BetweenDates(DateTime sDate, DateTime eDate)=>
            (from p in ProjectList
             where p.StartDate >= sDate && StartDate <= eDate
             select p).ToList();

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

        public static List<Project> MoreThanSixWeeks() =>
            (from p in ProjectList
             where (p.Duration / 5) > 6
             select p).ToList();

        public static int GetDuration(DateTime sDate, DateTime eDate) 
        {
            int totalDays = 0;
            while(sDate!=eDate)
            {
                if(sDate.IsWorkingDay()) //if date is a working day then it will count the days. 
                {
                    totalDays++;
                }
                sDate= sDate.AddDays(1); //loop control variable. 
            }
            return totalDays;
        }

        /// <summary>
        /// Method that returns a list of projects which have ended. 
        /// </summary>
        /// <returns></returns>
        public static List<Project> GetProjectsEnded()
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

        public static List<Project> Completed() =>
            (from p in ProjectList
             where p.EndDate < DateTime.Today
             select p).ToList();

        /// <summary>
        /// Method that returns a list of projects which were started in a specific month. 
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public static List<Project> GetProjectsMonth(int month) 
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

        //•	Create a delegate and event that will give notification when a project has 5 days left before it’s end date.

        public void AddProjects()
        {
            string strInsert = $"Insert into tblProject Values('{ProjectCode}', '{projectName}', '{StartDate.ToString("yyyy-MM-dd")}','{EndDate.ToString("yyyy-MM-dd")}', {Duration}, {EstimatedCost})";

            using(SqlConnection con=Connections.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(strInsert, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Project> AllProjects()
        {
            List<Project> ls = new();
            using(SqlConnection con=Connections.GetConnection())
            {
                string strSelect = "select * from tblProject";
                SqlCommand cmd = new SqlCommand(strSelect, con);
                con.Open();
                using(SqlDataReader reader= cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ls.Add(new((string)reader[0],(string)reader[1], Convert.ToDateTime(reader[2]), 
                            Convert.ToDateTime(reader[3]), (int)reader[4], (double)reader[5]));
                    }
                }
            }
            return ls;
        }
    }
}
