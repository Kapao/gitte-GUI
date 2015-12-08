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
using GUIApplication.SellerServiceReference;
using Seller = GUIApplication.SellerServiceReference.Seller;

namespace GUIApplication
{
    /// <summary>
    /// Interaction logic for ShowSellerTest.xaml
    /// </summary>
    public partial class ShowSellerTest : Window
    {
        static ISellerService iSeller = new SellerServiceClient();
        public ShowSellerTest()
        {
            InitializeComponent();

        }

        private void sellerData_Loaded(object sender, RoutedEventArgs e)
        {
            List<Seller> sellers = iSeller.GetAllSellers();
            var grid = sender as DataGrid;
            grid.ItemsSource = sellers;
            foreach (Seller seller in sellers)
            {
                seller.Location.City += "Test";
            }
        }
    }
}
