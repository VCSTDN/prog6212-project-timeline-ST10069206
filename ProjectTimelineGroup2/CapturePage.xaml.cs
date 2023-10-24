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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTimelineGroup2
{
    /// <summary>
    /// Interaction logic for CapturePage.xaml
    /// </summary>
    public partial class CapturePage : Page
    {
        //DateTime semesterDate = Convert.ToDateTime("04-09-2023");

        public CapturePage()
        {
            InitializeComponent();
            //dateStart.DisplayDateStart = semesterDate;
            //dateStart.DisplayDateEnd = semesterDate.AddDays(5 * 7);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Project p = new Project(txtCode.Text, txtName.Text, dateStart.SelectedDate.Value, dateEnd.SelectedDate.Value,Convert.ToDouble(txtRate.Text));
                
                //Project.ProjectList.Add(p);
                p.AddProjects();
                txtEC.Text = p.EstimatedCost.ToString();
                txtDuration.Text = p.Duration.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, this.Title, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtCode.Clear();
            txtDuration.Clear();
            txtEC.Clear();
            txtName.Clear();
            txtRate.Clear();
            dateEnd.SelectedDate = null; 
            dateStart.SelectedDate=null;
        }

        private void txtCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtCode.Text.Trim().Length > 0) 
            {
                btnSave.IsEnabled = true;
            }
        }
    }
}
