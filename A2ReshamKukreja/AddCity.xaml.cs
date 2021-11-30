using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace A2ReshamKukreja
{
    /// <summary>
    /// Interaction logic for AddCity.xaml
    /// </summary>
    public partial class AddCity : Window
    {
        public AddCity()
        {
            InitializeComponent();

        }

        public void FillCity()
        {
            SqlData.tblCities = SqlData.adpCities.GetCities();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (cmbCountry.SelectedIndex == -1)
            {
                lblCountry.Content = "* This Field is required.";
            }
            else if (txtCity.Text.Equals(""))
            {
                lblCity.Content = "* This Field is required.";
            }
            else
            {
                // database COde goes here

               //  FillContinent();

                string cityName = txtCity.Text;
                bool isCap = (bool)chkCap.IsChecked;
                string pop = txtPop.Text;
                DataRowView drv = (DataRowView)cmbCountry.SelectedItem; // error while changing continent
                string a = drv["CountryId"].ToString();

                // name, capital, population, id
                SqlData.adpCities.Insert(cityName, isCap, pop, Convert.ToInt32(a));

                FillCity();

                MessageBox.Show("New Country Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            this.Close();
        }


        private void cmbCountry_Loaded(object sender, RoutedEventArgs e)
        {

            SqlData.tblCountries = SqlData.adpCountries.GetCountries();
            cmbCountry.ItemsSource = SqlData.tblCountries;
            cmbCountry.DisplayMemberPath = "CountryName";

        }

      
    }
}
