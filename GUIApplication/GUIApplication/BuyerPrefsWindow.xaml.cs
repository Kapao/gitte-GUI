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
using System.Text.RegularExpressions;
using GUIApplication.ServiceReference;

namespace GUIApplication
{
    /// <summary>
    /// Interaction logic for BuyerPrefsWindow.xaml
    /// </summary>
    public partial class BuyerPrefsWindow : Window
    {
        private Buyer buyer;
        private CreateSeller createWindow;
        public BuyerPrefsWindow(Buyer b, CreateSeller cs)
        {
            buyer = b;
            createWindow = cs;
            InitializeComponent();
        }

        private void BtnNext(object sender, RoutedEventArgs e)
        {
            try
            {
                buyer.MinPrice = Convert.ToDouble(txtPriceMin.Text);
                buyer.MaxPrice = Convert.ToDouble(txtPriceMax.Text);
                buyer.ProbertySizeMin = Convert.ToDouble(txtPropertyMin.Text);
                buyer.ProbertySizeMax = Convert.ToDouble(txtPropertyMax.Text);
                buyer.DesiredRoomsMin = Convert.ToDouble(txtRoomsMin.Text);
                buyer.DesiredRoomsMax = Convert.ToDouble(txtRoomsMax.Text);
                buyer.LotSizeMin = Convert.ToDouble(txtLotMin.Text);
                buyer.LotSizeMax = Convert.ToDouble(txtLotMax.Text);
                buyer.Misc = txtMisc.Text;
                BuyerFinishWindow window = new BuyerFinishWindow(buyer, createWindow, this);
                this.Topmost = false;
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Topmost = true;
                window.Show();
            }
            catch(Exception)
            {
                MessageBox.Show("Alle felter skal være udfyldt korrekt.");
            }
            
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            createWindow.Close();
            this.Close();
        }

        private void BtnBack(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtPriceMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPriceMin.Text == "Pris fra")
            {
                txtPriceMin.Text = "";
            }
        }

        private void txtPriceMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPriceMax.Text == "Pris til")
            {
                txtPriceMax.Text = "";
            }
        }

        private void txtPropertyMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPropertyMin.Text == "Boligareal fra")
            {
                txtPropertyMin.Text = "";
            }
        }

        private void txtRoomsMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtRoomsMin.Text == "Værelser fra")
            {
                txtRoomsMin.Text = "";
            }
        }

        private void txtLotMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLotMin.Text == "Grundareal fra")
            {
                txtLotMin.Text = "";
            }
        }

        private void txtPropertyMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPropertyMax.Text == "Boligareal til")
            {
                txtPropertyMax.Text = "";
            }
        }

        private void txtRoomsMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtRoomsMax.Text == "Værelser til")
            {
                txtRoomsMax.Text = "";
            }
        }

        private void txtLotMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLotMax.Text == "Grundareal til")
            {
                txtLotMax.Text = "";
            }
        }

        private void RegExDouble(string e)
        {
            Regex regex = new Regex(@"^\d+([\,]?\d+)?$");
            if(!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '111,111'. \nMaks 1 komma!");
            }
        }

        private void RegExInt(string e)
        {
            Regex regex = new Regex(@"^\d+$");
            if (!regex.IsMatch(e))
            {
                MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '123'.");
            }
        }

        private void txtPriceMin_LostFocus(object sender, RoutedEventArgs e)
        {
            string minPrice = txtPriceMin.Text;
            RegExDouble(minPrice);
        }

        private void txtPriceMax_LostFocus(object sender, RoutedEventArgs e)
        {
            string maxPrice = txtPriceMax.Text;
            RegExDouble(maxPrice);
        }

        private void txtPropertyMin_LostFocus(object sender, RoutedEventArgs e)
        {
            string minProp = txtPropertyMin.Text;
            RegExDouble(minProp);
        }

        private void txtPropertyMax_LostFocus(object sender, RoutedEventArgs e)
        {
            string maxProp = txtPropertyMax.Text;
            RegExDouble(maxProp);
        }
        
        private void txtRoomsMin_LostFocus(object sender, RoutedEventArgs e)
        {
            string minRooms = txtRoomsMin.Text;
            RegExInt(minRooms);
        }

        private void txtRoomsMax_LostFocus(object sender, RoutedEventArgs e)
        {
            string maxRooms = txtRoomsMax.Text;
            RegExInt(maxRooms);
        }

        private void txtLotMin_LostFocus(object sender, RoutedEventArgs e)
        {
            string lotMin = txtLotMin.Text;
            RegExDouble(lotMin);
        }

        private void txtLotMax_LostFocus(object sender, RoutedEventArgs e)
        {
            string lotMax = txtLotMax.Text;
            RegExDouble(lotMax);
        }
    }
}
