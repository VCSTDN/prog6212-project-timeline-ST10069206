using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLibrary
{
    public class Employee
    {
        public string EmployeeNo { get;set; }
        public string FirstName { get;set; }
        public string LastName { get;set; }
        public string Password { get;set; }
        public double Salary { get;set; }
        public string EmpType { get;set; }

        public Employee(string employeeNo, string firstName, string lastName, string password, double salary, string empType)
        {
            EmployeeNo = employeeNo;
            FirstName = firstName;
            LastName = lastName;
            Password = password;
            Salary = salary;
            EmpType = empType;
        }

        public static Employee GetEmployee(string empNo)
        {
            Employee em = null;
            using (SqlConnection con= Connections.GetConnection())
            {
                string strSelect = $"SELECT * FROM tblEmployee where EmployeeNo='{empNo}'";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                using (SqlDataReader rd = cmdSelect.ExecuteReader())
                {
                    while (rd.Read())
                    {
                        em = new Employee(rd.GetString(0), rd.GetString(1), rd.GetString(2),
                            rd.GetString(4), rd.GetDouble(3), rd.GetString(5));
                    }
                }
            }
            return em;
            
        }
    }


}
