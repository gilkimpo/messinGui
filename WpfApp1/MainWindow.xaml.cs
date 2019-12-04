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
using System.Globalization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Team> team = new List<Team>();
        public List<Function> function = new List<Function>();
        public MainWindow()
        {
            InitializeComponent();
            this.Title = "Whaddup suckas!";
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //just making list and class in main for now rather than creating class for itself

            team.Add(new Team { FirstName = "Joseph" });
            team.Add(new Team { FirstName = "Randy" });
            team.Add(new Team { FirstName = "Gina" });
            team.Add(new Team { FirstName = "Gil" });
            mycombox.ItemsSource = team;
            function.Add(new Function { DoSomethingPlease = "This should do something" });
            function.Add(new Function { DoSomethingPlease = "i wish this would do somthing too" });
            function.Add(new Function { DoSomethingPlease = "guess what i'm thinking..." });
            otherbox.ItemsSource = function;
        }
        public class Function
        {
            public string DoSomethingPlease { get; set; }
        }
        public class Team
        {
            public string FirstName { get; set; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mmsg= MessageBox.Show("u wanna check out book?", "hello", MessageBoxButton.YesNoCancel);
            if (mmsg==MessageBoxResult.Yes)
            {
                //this is all practice stuff, not very good execution of logic
                Close();
            }
            else
            {
                
            }                       
                   
        }
       //testing how to get mult buttons on

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("u wanna return book", "HI again", MessageBoxButton.YesNoCancel);

        }
    }
}
