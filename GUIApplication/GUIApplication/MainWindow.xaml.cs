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
using GUIApplication.ServiceReference;

namespace GUIApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static IService iServ = new ServiceClient();
        private User currentUser;

        public MainWindow(User user)
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            currentUser = user;
            InitializeComponent();
            txtUser.Text = currentUser.Name + ", " + DateTime.Today.Day + "/" + DateTime.Today.Month + "-" + DateTime.Today.Year;
        }

        private void sellerData_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateSellerDatagrid(sender);
        }

        //Viser info om en kunde, både fra buyer og seller tab
        private void BtnVisInfo(object sender, RoutedEventArgs e)
        {
            if (buyerTab.IsSelected)
            {
                Buyer buyer = (Buyer)buyerData.SelectedItem;
                if (buyer != null)
                {
                    Location loc = iServ.GetLocation(buyer.ZipCode);
                    MessageBox.Show("KøberID: " + buyer.Id + "\nNavn: " + buyer.Name + "\nAdresse: " + buyer.Address + "\nPostnummer: " + buyer.ZipCode + " By: " + loc.City + "\nTelefon: " + buyer.Phone + "\nMobil: " + buyer.Mobile + "\nEmail: " + buyer.Email + "\nMisc: " + buyer.Misc);
                }
            }
            else
            {
                Seller seller = (Seller)sellerData.SelectedItem;
                if (seller != null)
                {
                    Location loc = iServ.GetLocation(seller.ZipCode);
                    MessageBox.Show("SælgerID: " + seller.Id + "\nNavn: " + seller.Name + "\nAdresse: " + seller.Address + "\nPostnummer: " + seller.ZipCode + " By: " + loc.City + "\nTelefon: " + seller.Phone + "\nMobil: " + seller.Mobile + "\nEmail: " + seller.Email + "\nMisc: " + seller.Misc);
                }
            }
        }

        //Opret kunde; buyer eller seller
        private void BtnCreateCustomer(object sender, RoutedEventArgs e)
        {
            CreateSeller window = new CreateSeller();
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Topmost = true;
            window.Show();
        }

        private void buyerData_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateBuyerDatagrid(sender);
        }

        private void BtnUpdateCustomer(object sender, RoutedEventArgs e)
        {
            if (buyerTab.IsSelected)
            {
                Buyer buyer = (Buyer)buyerData.SelectedItem;
                if (buyer != null)
                {
                    UpdateBuyer window = new UpdateBuyer(buyer);
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Topmost = true;
                    window.Show();
                }
            }
            else
            {
                Seller seller = (Seller)sellerData.SelectedItem;
                if (seller != null)
                {
                    UpdateSeller window = new UpdateSeller(seller);
                    window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    window.Topmost = false;
                    window.Show();
                }
            }
        }

        private void BtnDeleteCustomer(object sender, RoutedEventArgs e)
        {
            if (buyerTab.IsSelected)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Er du sikker?", "Slet køber", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Buyer buyer = (Buyer)buyerData.SelectedItem;
                    if (buyer != null)
                    {
                        iServ.DeleteBuyer(buyer);
                        buyerData.ItemsSource = iServ.GetAllBuyers();
                    }
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Er du sikker?", "Slet sælger", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Seller seller = (Seller)sellerData.SelectedItem;
                    if (seller != null)
                    {
                        iServ.DeleteSeller(seller);
                        sellerData.ItemsSource = iServ.GetAllSellers();
                    }
                }
            }
        }
        private void UpdateBuyerDatagrid(object sender)
        {
            List<Buyer> buyers = iServ.GetAllBuyers().ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = buyers;
        }
        private void UpdateSellerDatagrid(object sender)
        {
            List<Seller> sellers = iServ.GetAllSellers().ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = sellers;
        }

        private void BtnCreateAppointment(object sender, RoutedEventArgs e)
        {
            CreateAppointment window = new CreateAppointment(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Topmost = true;
            window.Show();
        }

        private void calendar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DateTime date = calendar.SelectedDate.Value;
            List<Appointment> appointments = currentUser.Appointments.ToList();
            List<Appointment> appointmentsToShow = new List<Appointment>();
            foreach (Appointment ap in appointments)
            {
                if (ap.Date == date)
                {
                    appointmentsToShow.Add(ap);
                }
            }
            var grid = appointmentData as DataGrid;
            grid.ItemsSource = appointmentsToShow;
        }

        private void appointmentData_Loaded(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Today;
            List<Appointment> appointments = currentUser.Appointments.ToList();

            List<Appointment> appointmentsToShow = new List<Appointment>();
            foreach (Appointment ap in appointments)
            {
                if (ap.Date >= date)
                {
                    appointmentsToShow.Add(ap);
                }
            }
            var grid = appointmentData as DataGrid;
            grid.ItemsSource = appointmentsToShow;

        }

        private void Button_VisInfo(object sender, RoutedEventArgs e)
        {
            Appointment ap = (Appointment)appointmentData.SelectedItem;
            if (ap != null)
            {
                MessageBox.Show(appointmentInfo());
            }
        }

        private string appointmentInfo()
        {
            Appointment ap = (Appointment)appointmentData.SelectedItem;
            string formatDate = "dd.MM-yy";
            string formatTime = "HH.mm";
            string info = "ID: " + ap.Id + "\nDato: " + ap.Date.ToString(formatDate) + "\tVarighed: "
                   + ap.StarTime.ToString(formatTime) + " - " + ap.EndTime.ToString(formatTime) + "\nKategori: "
                   + ap.Category + "\nBeskrivelse: " + ap.Description + "\nStatus: " + ap.Status;
            if (ap.Buyer != null)
            {
                info += "\nKunde(køber): " + ap.Buyer.Name;
            }
            else if (ap.Seller != null)
            {
                info += "\nKunde(sælger): " + ap.Seller.Name;
            }
            return info;
        }

        private void BtnShowAllAppointments(object sender, RoutedEventArgs e)
        {
            appointmentData.ItemsSource = iServ.GetAllAppointments();
        }

        private void Button_DeleteAppointment(object sender, RoutedEventArgs e)
        {
            Appointment appointment = (Appointment)appointmentData.SelectedItem;
            if (appointment != null)
            {
                currentUser.Appointments.Remove(appointment);
                iServ.DeleteAppointment(appointment);
                appointmentData_Loaded(sender, e);
            }
        }

        public User CurrentUser 
        { 
            get {return currentUser;}
            set { currentUser = value; }
        }

        private void Button_UpdateAppointment(object sender, RoutedEventArgs e)
        {

        }
    }
}
