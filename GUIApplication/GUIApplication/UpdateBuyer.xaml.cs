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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateBuyer : Window
    {
        static IService iServ = new ServiceClient();

        private Buyer buyer;
        public UpdateBuyer(Buyer buyer)
        {
            this.buyer = buyer;
            InitializeComponent();
            AddText();
        }

        private void AddText()
        {
            txtName.Text = buyer.Name;
            txtAddress.Text = buyer.Address;
            txtZipCode.Text = buyer.ZipCode;
            txtPhone.Text = buyer.Phone;
            txtMobile.Text = buyer.Mobile;
            txtEmail.Text = buyer.Email;
            txtMisc.Text = buyer.Misc;

            txtPriceMin.Text = Convert.ToString(buyer.MinPrice);
            txtPriceMax.Text = Convert.ToString(buyer.MaxPrice);
            txtPropertyMin.Text = Convert.ToString(buyer.LotSizeMin);
            txtPropertyMax.Text = Convert.ToString(buyer.LotSizeMax);
            txtRoomsMin.Text =  Convert.ToString(buyer.DesiredRoomsMin);
            txtRoomsMax.Text = Convert.ToString(buyer.DesiredRoomsMax);
            txtLotMin.Text =  Convert.ToString(buyer.LotSizeMin);
            txtLotMax.Text =  Convert.ToString(buyer.LotSizeMax);
            if(buyer.InRKI == true)
            {
                checkInRKI.IsChecked = true;
            }
            if(buyer.BuyerApproved == true)
            {
                checkBuyerApproved.IsChecked = true;
                txtBank.Text = buyer.Bank;
                txtApprovedAmount.Text = Convert.ToString(buyer.ApprovedAmount);
            }
            if(buyer.OwnesHouse == true)
            {
                checkOwner.IsChecked = true;
            }
            if(buyer.LivesForRent == true)
            {
                checkRents.IsChecked = true;
            }
        }

        private void txtZipCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text == "")
            {
                txtZipCode.Text = "Postnummer";
            }
            else
            {
                try
                {
                    lblCity.Content = iServ.GetLocation(txtZipCode.Text).City;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ugyldigt postnummer!");
                }
            }
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string address = txtAddress.Text;
            string zipCode = txtZipCode.Text;
            string phone = txtPhone.Text;
            string mobile = txtMobile.Text;
            string email = txtEmail.Text;
            string misc = txtMisc.Text;
            double minPrice = Convert.ToDouble(txtPriceMin.Text);
            double maxPrice = Convert.ToDouble(txtPriceMax.Text);
            double minHouseSize = Convert.ToDouble(txtPropertyMin.Text);
            double maxHouseSize = Convert.ToDouble(txtPropertyMax.Text);
            int minRooms = Convert.ToInt32(txtRoomsMin.Text);
            int maxRooms = Convert.ToInt32(txtRoomsMax.Text);
            double minLot = Convert.ToDouble(txtLotMin.Text);
            double maxLot = Convert.ToDouble(txtLotMax.Text);
            bool inRKI = false;
            bool buyerApproved = false;
            bool owner = false;
            bool rents = false;
            string bank = buyer.Bank;
            if (checkInRKI.IsChecked == true)
            {
                inRKI = true;
            }
            if (checkBuyerApproved.IsChecked == true)
            {
                buyerApproved = true;
                bank = txtBank.Text;
                double approvedAmount = Convert.ToDouble(txtApprovedAmount.Text);
            }
            if (checkOwner.IsChecked == true)
            {
                owner = true;
            }
            if (checkRents.IsChecked == true)
            {
                rents = true;
            }

            iServ.UpdateBuyer(buyer, buyer.Properties, name, address, zipCode, phone, mobile, email, misc, buyer.EstateType, minPrice, maxPrice,
                minLot, maxLot, minHouseSize, maxHouseSize, minRooms, maxRooms, buyer.Locations, buyer.OtherPref, buyer.ContactAllowedByBoligOne, buyer.ContactAllowedByReal
                , buyer.AllowedEmailSpam, inRKI, buyerApproved, bank, owner, rents);



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
    }
}
