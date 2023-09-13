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
    /// Interaction logic for Filter.xaml
    /// </summary>
    public partial class Filter : Page
    {
        public Filter()
        {
            InitializeComponent();
        }

        public void displayList(List<Project> projects)
        {
            foreach (Project p in projects)
            {
                dgvDisplay2.Items.Add(p);
            }
        }

        public void cmbFilter_SelectedChanged(object sender, EventArgs e)
        {
            dgvDisplay2.Items.Clear();
            List<Project> projects = new List<Project>();
            Project pr=new Project();
            if (cmbFilter.SelectedIndex ==0) 
            {
                projects = Project.ProjectList;
                displayList(projects);
            }
            else if(cmbFilter.SelectedIndex == 1) 
            {
                pr = pr["P101"];
                dgvDisplay2.Items.Add(pr);
            }
            else if (cmbFilter.SelectedIndex == 2)
            {
                projects = Project.Completed();
                displayList(projects);
            }
            else if (cmbFilter.SelectedIndex == 3)
            {
                projects = Project.MoreThanSixWeeks();
                displayList(projects);
            }
            else if (cmbFilter.SelectedIndex == 4)
            {
                projects = pr.BetweenDates(Convert.ToDateTime("06-05-2023"),Convert.ToDateTime("06-12-2023"));
                displayList(projects);
            }
        }
        
    }
}
