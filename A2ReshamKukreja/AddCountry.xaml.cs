using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace A2ReshamKukreja
{
    /// <summary>
    /// Interaction logic for AddCountry.xaml
    /// </summary>
    public partial class AddCountry : Window
    {
        public AddCountry()
        {
            InitializeComponent();
        }

        public void FillCountry()
        {
            SqlData.tblCountries = SqlData.adpCountries.GetCountries();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(cmbContinents.SelectedIndex == -1 )
            {
                lblContinent.Content = "* This Field is required.";
            }
            else if(txtCountry.Text.Equals(""))
            {
                lblCountry.Content = "* This Field is required.";
            }
            else
            {
                // database COde goes here

                string countryName = txtCountry.Text;
                string lang = txtLang.Text;
                string curr = txtCurr.Text;
                DataRowView drv = (DataRowView)cmbContinents.SelectedItem; // error while changing continent
                string a = drv["ContinentId"].ToString();

                // name, lang, curr, id
                SqlData.adpCountries.InsertQuery(countryName, lang, curr, Convert.ToInt32(a));

                FillCountry();

                MessageBox.Show("New Country Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cmbContinent_Loaded(object sender, RoutedEventArgs e)
        {
            SqlData.tblContinents = SqlData.adpContinents.GetContinents();
            cmbContinents.ItemsSource = SqlData.tblContinents;
            cmbContinents.DisplayMemberPath = "ContinentName";
        }
    }
}
