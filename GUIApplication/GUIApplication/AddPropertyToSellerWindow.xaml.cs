﻿using System;
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
    /// Interaction logic for AddPropertyToSellerWindow.xaml
    /// </summary>
    public partial class AddPropertyToSellerWindow : Window
    {
        private Seller seller;
        private CreateSeller createSeller;
        static IService iServ = new ServiceClient();
        public AddPropertyToSellerWindow(Seller s, CreateSeller cs)
        {
            createSeller = cs;
            seller = s;
            InitializeComponent();
        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            createSeller.Close();
            this.Close();
        }

        private void BtnAddProperty(object sender, RoutedEventArgs e)
        {

            Property property = new Property()
            {
                Address = txtAddress.Text,
                ZipCode = txtZipCode.Text,
                Type = txtType.Text,
                Rooms = Convert.ToInt32(txtRooms.Text),
                Floors = Convert.ToInt32(txtFloors.Text),
                Price = Convert.ToDouble(txtPrice.Text),
                PropertySize = Convert.ToDouble(txtLotSize.Text),
                HouseSize = Convert.ToDouble(txtHouseSize.Text),
                ConstructionYear = Convert.ToInt32(txtConstructionYear.Text)
            };
            List<Property> props = new List<Property>();
            props.Add(property);
            seller.Properties = props;
            iServ.InsertSeller(seller);
            createSeller.Close();
            this.Close();

        }

        private void BtnSearchAddress(object sender, RoutedEventArgs e)
        {
            Property property = iServ.GetAllPropertiesFromSeller(seller).FirstOrDefault();
            txtAddress.Text = property.Address;
            txtZipCode.Text = property.ZipCode;
            txtRooms.Text = property.Rooms.ToString();
            txtFloors.Text = property.Floors.ToString();
            txtHouseSize.Text = property.HouseSize.ToString();
            txtLotSize.Text = property.PropertySize.ToString();
            txtPrice.Text = property.Price.ToString();
            txtType.Text = property.Type.ToString();
            txtConstructionYear.Text = property.ConstructionYear.ToString();
        }

        private void RegExDouble(string e)
        {
            Regex regex = new Regex(@"^\d+([\,]?\d+)?$");
            if (!regex.IsMatch(e))
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

        private void txtZipCode_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text == "")
            {
                txtZipCode.Text = "Postnummer";
            }
            else
            {
                string zipCode = txtZipCode.Text;
                Regex regex = new Regex(@"^\d+$");
                if (!regex.IsMatch(zipCode))
                {
                    MessageBox.Show("Textboxen indeholder ugyldig information.\nTextboxen tager imod formatet '1234'.");
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
        }

        private void txtZipCode_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtZipCode.Text == "Postnummer")
                txtZipCode.Text = "";
        }

        private void txtSearchProperty_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtSearchProperty.Text == "Adresse")
                txtSearchProperty.Text = "";
        }

        private void txtAddress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtAddress.Text == "Adresse")
                txtAddress.Text = "";
        }

        private void txtRooms_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtRooms.Text == "Værelser")
                txtRooms.Text = "";
        }

        private void txtFloors_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtFloors.Text == "Etager")
                txtFloors.Text = "";
        }

        private void txtHouseSize_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtHouseSize.Text == "Boligareal")
                txtHouseSize.Text = "";
        }

        private void txtLotSize_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtLotSize.Text == "Grundareal")
                txtLotSize.Text = "";
        }

        private void txtPrice_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtPrice.Text == "Pris")
                txtPrice.Text = "";
        }

        private void txtConstructionYear_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtConstructionYear.Text == "Byggeår")
                txtConstructionYear.Text = "";
        }

        private void txtType_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtType.Text == "Boligtype")
                txtType.Text = "";
        }

        private void txtRooms_LostFocus(object sender, RoutedEventArgs e)
        {
            string rooms = txtRooms.Text;
            RegExInt(rooms);
        }

        private void txtFloors_LostFocus(object sender, RoutedEventArgs e)
        {
            string floors = txtFloors.Text;
            RegExInt(floors);
        }

        private void txtHouseSize_LostFocus(object sender, RoutedEventArgs e)
        {
            string houseSize = txtHouseSize.Text;
            RegExDouble(houseSize);
        }

        private void txtLotSize_LostFocus(object sender, RoutedEventArgs e)
        {
            string lotSize = txtLotSize.Text;
            RegExDouble(lotSize);
        }

        private void txtPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            string price = txtPrice.Text;
            RegExDouble(price);
        }

        private void txtConstructionYear_LostFocus(object sender, RoutedEventArgs e)
        {
            string constructionYear = txtConstructionYear.Text;
            RegExInt(constructionYear);
        }

    }
}
