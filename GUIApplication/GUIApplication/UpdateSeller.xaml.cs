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
    /// Interaction logic for UpdateSeller.xaml
    /// </summary>
    public partial class UpdateSeller : Window
    {
        static IService iServ = new ServiceClient();

        //static IPropertyService iProp = new PropertyServiceClient();
        private Seller seller;
        private Property property;
        private List<Property> properties;
        public UpdateSeller(Seller s)
        {
            InitializeComponent();

            property = new Property();
            seller = s;
            properties = seller.Properties;
            AddText();
        }

        private void AddText()
        {
            Property prop = properties.FirstOrDefault();
            property.Id = prop.Id;
            txtName.Text = seller.Name;
            txtAddress.Text = seller.Address;
            txtZipCode.Text = seller.ZipCode;
            lblCity.Content = iServ.GetLocation(seller.ZipCode).City;
            txtPhone.Text = seller.Phone;
            txtMobile.Text = seller.Mobile;
            txtEmail.Text = seller.Email;
            txtMisc.Text = seller.Misc;

            txtAddressProperty.Text = prop.Address;
            txtZipCodeProperty.Text = prop.ZipCode;
            txtRooms.Text = prop.Rooms.ToString();
            txtFloors.Text = prop.Floors.ToString();
            txtHouseSize.Text = prop.HouseSize.ToString();
            txtLotSize.Text = prop.PropertySize.ToString();
            txtPrice.Text = prop.Price.ToString();
            txtConstructionYear.Text = prop.ConstructionYear.ToString();
            txtType.Text = prop.Type;

        }

        private void BtnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdate(object sender, RoutedEventArgs e)
        {
            property.Address = txtAddressProperty.Text;
            property.ZipCode = txtZipCodeProperty.Text;
            property.Type = txtType.Text;
            property.Rooms = Convert.ToInt32(txtRooms.Text);
            property.Floors = Convert.ToInt32(txtFloors.Text);
            property.HouseSize = Convert.ToDouble(txtHouseSize.Text);
            property.PropertySize = Convert.ToDouble(txtLotSize.Text);
            property.Price = Convert.ToDouble(txtPrice.Text);
            property.ConstructionYear = Convert.ToInt32(txtConstructionYear.Text);


            string name = txtName.Text;
            string address = txtAddress.Text;
            string zipCode = txtZipCode.Text;
            string phone = txtPhone.Text;
            string mobile = txtMobile.Text;
            string email = txtEmail.Text;
            string misc = txtMisc.Text;
            properties.Clear();
            properties.Add(property);
            try
            {
                iServ.UpdateSeller(seller, properties, name, address, zipCode, phone, mobile, email, misc);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fejl");
            }
            this.Close();
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

        private void txtZipCodeProperty_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtZipCodeProperty.Text == "")
            {
                txtZipCodeProperty.Text = "Postnummer";
            }
            else
            {
                try
                {
                    lblCityProperty.Content = iServ.GetLocation(txtZipCodeProperty.Text).City;
                }
                catch (Exception)
                {
                    MessageBox.Show("Ugyldigt postnummer!");
                }
            }
        }


    }
}
