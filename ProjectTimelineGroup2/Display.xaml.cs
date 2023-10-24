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
    public partial class Display : Page
    {
        public Display()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dgvDisplay.Items.Clear();
            Project pr=new Project();
            foreach(Project p in pr.AllProjects())
            {
                dgvDisplay.Items.Add(p);
            }
        }
    }
}
