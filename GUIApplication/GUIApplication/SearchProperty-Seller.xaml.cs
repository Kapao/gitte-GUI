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
    /// Interaction logic for SearchProperty_Seller.xaml
    /// </summary>
    public partial class SearchProperty_Seller : Window
    {
        static IService iServ = new ServiceClient();

        private CreateAppointment sourceWin;

        public SearchProperty_Seller(CreateAppointment source)
        {
            InitializeComponent();
            this.sourceWin = source;
        }

        private void Button_Search(object sender, RoutedEventArgs e)
        {
            Property property;
            Seller seller;
            if (txtPropertyAddress.Text != "")
            {
                try
                {
                    property = iServ.GetProperty(txtPropertyAddress.Text);
                    seller = iServ.GetSellerById(property.SellerID);
                    txtZipcode.Text = property.ZipCode;
                    txtMtkNr.Text = "Matrikelnummer her";
                    txtSellerName.Text = seller.Name;
                    txtSellerAddress.Text = seller.Address;
                    txtSellerPhone.Text = seller.Phone;
                    txtSellerMobile.Text = seller.Mobile;
                }
                catch (Exception)
                {
                    MessageBox.Show("Sælger/ejendom ikke fundet.");
                }
            }
            else if (txtSellerPhone.Text != "")
            {
                try
                {
                    seller = iServ.GetSellerByPhone(txtSellerPhone.Text);
                    property = iServ.GetPropertyBySellerID(seller.Id);
                    txtPropertyAddress.Text = property.Address;
                    txtZipcode.Text = property.ZipCode;
                    txtMtkNr.Text = "Matrikelnummer her";
                    txtSellerName.Text = seller.Name;
                    txtSellerAddress.Text = seller.Address;
                    txtSellerMobile.Text = seller.Mobile;
                }
                catch (Exception)
                {
                    MessageBox.Show("Sælger/ejendom ikke fundet.");
                }
            }
            else if (txtSellerMobile.Text != "")
            {
                try
                {
                    seller = iServ.GetSellerByMobile(txtSellerMobile.Text);
                    property = iServ.GetPropertyBySellerID(seller.Id);
                    txtPropertyAddress.Text = property.Address;
                    txtZipcode.Text = property.ZipCode;
                    txtMtkNr.Text = "Matrikelnummer her";
                    txtSellerName.Text = seller.Name;
                    txtSellerAddress.Text = seller.Address;
                    txtSellerPhone.Text = seller.Phone;
                }
                catch (Exception)
                {
                    MessageBox.Show("Sælger/ejendom ikke fundet.");
                }
            }
            else
            {
                MessageBox.Show("Indtast beliggenhed eller sælgernummer");
            }
        }

        private void Button_Annuller(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Choose(object sender, RoutedEventArgs e)
        {
            sourceWin.txtBuyerSubject.Text = txtSellerPhone.Text;
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
            Regex regex = new Regex(@"^\d+$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '1234'.");
            }
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtZipcode_LostFocus(object sender, RoutedEventArgs e)
        {
            string zipCode = txtZipcode.Text;
            RegExZipCode(zipCode);
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtSellerPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            string phone = txtSellerPhone.Text;
            RegExInt(phone);
        }

        //LostFocus Method that calls the regularexpression-method for the specific textbox
        private void txtSellerMobile_LostFocus(object sender, RoutedEventArgs e)
        {
            string mobile = txtSellerMobile.Text;
            RegExInt(mobile);
        }
    }
}
