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

namespace A2ReshamKukreja
{
    /// <summary>
    /// Interaction logic for AddContinent.xaml
    /// </summary>
    public partial class AddContinent : Window
    {

       

       

        public AddContinent()
        {
            InitializeComponent();

            SqlData.adpContinents = new WorldDBDataSetTableAdapters.ContinentTableAdapter();
        }

        public void FillContinent()
        {
            SqlData.tblContinents = SqlData.adpContinents.GetContinents();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        


        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(txtContinetName.Text.Equals(""))
            {
                lblError.Content = " * This field is required";
            } else
            {

                FillContinent();

                string contName = txtContinetName.Text;

                SqlData.adpContinents.Insert(contName);

                FillContinent();


                // database COde goes here

                MessageBox.Show("New Continent Added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
