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
    /// Interaction logic for BuyerFinishWindow.xaml
    /// </summary>
    public partial class BuyerFinishWindow : Window
    {
        static IService iServ = new ServiceClient();
        private Buyer buyer;
        private BuyerPrefsWindow buyerWindow;
        private CreateSeller createWindow;
        public BuyerFinishWindow(Buyer b, CreateSeller cs, BuyerPrefsWindow w)
        {
            buyer = b;
            buyerWindow = w;
            createWindow = cs;
            InitializeComponent();
        }

        private void BtnSaveAndClose(object sender, RoutedEventArgs e)
        {
            if (checkInRKI.IsChecked == true)
            {
                buyer.InRKI = true;
            }
            if (checkBuyerApproved.IsChecked == true)
            {
                buyer.BuyerApproved = true;
                buyer.Bank = txtBank.Text;

                // INDSÆT BELØB HER - SKAL OPRETTES I MODELLEN BUYER  Convert.ToDouble(txtApprovedAmount.Text);
            }
            if (checkOwner.IsChecked == true)
            {
                buyer.OwnesHouse = true;
            }
            if (checkRents.IsChecked == true)
            {
                buyer.LivesForRent = true;
            }
            iServ.InsertBuyer(buyer);
            createWindow.Close();
            buyerWindow.Close();
            this.Close();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            createWindow.Close();
            buyerWindow.Close();
            this.Close();
        }

        private void BtnBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtApprovedAmount_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtApprovedAmount.Text == "Beløb")
            {
                txtApprovedAmount.Text = "";
            }
        }

        private void txtApprovedAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            string amount = txtApprovedAmount.Text;
            RegExDouble(amount);
        }
        private void RegExDouble(string e)
        {
            Regex regex = new Regex(@"^\d+([\,]?\d+)?$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '111,111'. \nMaks 1 komma!");
            }
        }
    }
}
