using ProjectLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectTimelineGroup2.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string empNo=txtEmpNo.Text;
            string pass=txtPassword.Password.ToString();

            Employee em = Employee.GetEmployee(empNo);
            if(em.EmployeeNo.Equals(empNo) &&em.Password.Equals(pass))
            {
                MessageBox.Show($"Hello {em.FirstName} {em.LastName}");
            }
            else
            {
                MessageBox.Show($"Employee ({empNo}) not found");
            }
        }
    }
}
