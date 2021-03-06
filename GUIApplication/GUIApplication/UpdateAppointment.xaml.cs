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
    /// Interaction logic for UpdateAppointment.xaml
    /// </summary>
    public partial class UpdateAppointment : Window
    {
        static IService iServ = new ServiceClient();

        private MainWindow sourceWin;
        private Appointment appointment;
        private Buyer buyer;
        private Seller seller;
        private User user;

        public UpdateAppointment(Appointment a, MainWindow source)
        {
            InitializeComponent();
            this.appointment = a;
            this.buyer = appointment.Buyer;
            this.seller = appointment.Seller;
            this.sourceWin = source;
            this.user = iServ.GetUserById(appointment.UserID);
            
            AddText();
        }

        private void AddText()
        {
            dpStartDate.Text = appointment.Date.ToString();
            cbCategory.SelectedItem = appointment.Category;
            txtSagsnr.Text = appointment.Id.ToString();
            
            if (seller != null)
            {
                txtBuyerSubject.Text = seller.Phone;
            }
            if (buyer != null)
            {
                txtBuyer.Text = buyer.Phone;
            }

            //txtSubject =
            txtBoxDescription.Text = appointment.Description;
            tpStartTime.Value = appointment.StarTime;
            tpEndTime.Value = appointment.EndTime;
        }



        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            user = (User)cbUser.SelectedItem;
            if (user == null)
            {
                MessageBox.Show("Vælg en bruger");
            }
            else
            {
                try
                {

                    string category = cbCategory.Text;
                    DateTime date = Convert.ToDateTime(dpStartDate.Text);
                    string description = txtBoxDescription.Text;
                    DateTime StartTime = tpStartTime.Value.Value;
                    DateTime EndTime = tpEndTime.Value.Value;
                    string status = "I gang";
                    int id = user.Id;

                    iServ.UpdateAppointment(appointment, date, StartTime, EndTime, category, description, status);
                    sourceWin.UpdateUserList();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dato og tidspunkt for aftalen skal være udfyldt korrekt." + ex);
                }
            }
        }

        private void cbUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbUser = sender as ComboBox;
        }

        private void cbUser_Loaded(object sender, RoutedEventArgs e)
        {
            List<User> users = iServ.GetAllUsers().ToList();
            cbUser.ItemsSource = users;
            cbUser.DisplayMemberPath = "Name";
        }

        private void cbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbCategory = sender as ComboBox;
        }

        private void cbCategory_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> categories = new List<string>();
            categories.Add("Andet");
            categories.Add("ERFA-Møde");
            categories.Add("Eksternt møde");
            categories.Add("Ferie");
            categories.Add("Formidlingsaftalemøde");
            categories.Add("Fremvisning");
            categories.Add("Fri");
            categories.Add("Fritekst");
            categories.Add("HUSK");
            categories.Add("Internt møde");
            categories.Add("Kursus");
            categories.Add("Købermøde");
            categories.Add("Logbog");
            categories.Add("Nordeakredit");
            categories.Add("Opfølgning til sælger");
            categories.Add("Realkreditvurdering");
            categories.Add("Skole");
            categories.Add("Sælgermøde");
            categories.Add("Vurdering");
            categories.Add("Åbent husarrangement");

            cbCategory.ItemsSource = categories;
        }

        private void btnSearchProperty_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SearchProperty_Seller(null, this);
            window.Owner = this;
            window.Show();
        }

        private void btnSearchBuyer_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SearchBuyerWindow(null, this);
            window.Owner = this;
            window.Show();
        }
    }
}
