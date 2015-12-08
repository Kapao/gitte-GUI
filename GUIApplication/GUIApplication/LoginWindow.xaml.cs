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
using GUIApplication.ServiceReference;


namespace GUIApplication
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        static IService iServ = new ServiceClient();
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void cbLogIn_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> users = iServ.GetAllUsers().ToList();
            cbLogIn.ItemsSource = users;
            cbLogIn.DisplayMemberPath = "Name";
        }

        private void btLogIn_Click(object sender, RoutedEventArgs e)
        {
            User userLogin = (User)cbLogIn.SelectedItem;
            //Her skal der laves et passwordcheck
            MainWindow window = new MainWindow(userLogin);
            window.Show();
            this.Close();
        }
    }
}
