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
using System.Text.RegularExpressions;

namespace GUIApplication
{
    /// <summary>
    /// Interaction logic for SearchBuyerWindow.xaml
    /// 
    /// </summary>
    public partial class SearchBuyerWindow : Window
    {
        static IService iServ = new ServiceClient();

        private CreateAppointment sourceWin;
        private Buyer buyer;
        private Location location;

        public SearchBuyerWindow(CreateAppointment source)
        {
            InitializeComponent();
            this.sourceWin = source;
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            if (txtPhone.Text != "")
            {
                buyer = iServ.GetBuyerByPhone(txtPhone.Text);
                location = iServ.GetLocation(buyer.ZipCode);
                txtName.Text = buyer.Name;
                txtAddress.Text = buyer.Address;
                txtZipCode.Text = buyer.ZipCode;
                txtCity.Text = location.City;
                txtMobile.Text = buyer.Mobile;
                txtEmail.Text = buyer.Email;
            }
            else if (txtMobile.Text != "")
            {
                buyer = iServ.GetBuyerByMobile(txtMobile.Text);
                location = iServ.GetLocation(buyer.ZipCode);
                txtName.Text = buyer.Name;
                txtAddress.Text = buyer.Address;
                txtZipCode.Text = buyer.ZipCode;
                txtCity.Text = location.City;
                txtPhone.Text = buyer.Phone;
                txtEmail.Text = buyer.Email;
            }
            else 
            {
                MessageBox.Show("Indtast telefon- eller mobilnummer på kunden");
            }
        }

        private void Button_Annuller(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Choose(object sender, RoutedEventArgs e)
        {
            sourceWin.txtBuyer.Text = txtPhone.Text;
            this.Close();
        }

        //The following method uses a regular expression that checks if a string is of the datatype int(if it includes digets)
        private void RegExInt(string e)
        {
            Regex regex = new Regex(@"^\d+$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '12345678'.");
            }
        }

        //The following method uses a regular expression that checks if a string looks like a valid email. "name@domain.subdomain"
        private void RegExEmail(string e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet 'navn@domæne.dk'.");
            }
        }

        //The following method uses a regular expression that checks if a string is of the datatype int, gives a messagebox for zipcode if not.
        private void RegExZipCode(string e)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '1234'.");
            }
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtZipCode_LostFocus(object sender, RoutedEventArgs e)
        {
            string zipCode = txtZipCode.Text;
            RegExZipCode(zipCode);
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            string phone = txtPhone.Text;
            RegExInt(phone);
        }


        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtMobile_LostFocus(object sender, RoutedEventArgs e)
        {
            string mobile = txtMobile.Text;
            RegExInt(mobile);
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            RegExEmail(email);
        }
    }
}
