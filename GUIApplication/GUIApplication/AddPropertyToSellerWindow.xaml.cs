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
    }
}