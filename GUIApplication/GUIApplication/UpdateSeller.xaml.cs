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
        private List<Property> properties;
        public UpdateSeller(Seller s)
        {
            InitializeComponent();

            seller = s;
            properties = seller.Properties;
            AddText();
        }

        private void AddText()
        {
            txtName.Text = seller.Name;
            txtAddress.Text = seller.Address;
            txtZipCode.Text = seller.ZipCode;
            lblCity.Content = iServ.GetLocation(seller.ZipCode).City;
            txtPhone.Text = seller.Phone;
            txtMobile.Text = seller.Mobile;
            txtEmail.Text = seller.Email;
            txtMisc.Text = seller.Misc;


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
            try
            {
                iServ.UpdateSeller(seller, properties, name, address, zipCode, phone, mobile, email, misc);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Fejl " + ex);
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


    }
}
