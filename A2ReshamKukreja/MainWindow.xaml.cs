using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace A2ReshamKukreja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        public MainWindow()
        {

            
            InitializeComponent();

            SqlData.adpCities = new WorldDBDataSetTableAdapters.CityTableAdapter();
            SqlData.adpContinents = new WorldDBDataSetTableAdapters.ContinentTableAdapter();
            SqlData.adpCountries = new WorldDBDataSetTableAdapters.CountryTableAdapter();


        }

        // to load returned rows in data table - tableContinents and storing them on ComboBox
        public void GetContinent()
        {
 
            SqlData.tblContinents = SqlData.adpContinents.GetContinents();
            cmbContinents.ItemsSource = SqlData.tblContinents;

        }

        // load returned rows in data table - tableCountries
        public void GetCountry()
        {
            SqlData.tblCountries = SqlData.adpCountries.GetCountries();
            
            
        }

        // load returned rows in data table - tableCities
        public void GetCity()
        {
            SqlData.tblCities = SqlData.adpCities.GetCities();
        }


        // this methods stores Continent Names from all returned Coulmns in ComboBOX, first it stores data in tblContinents. 
        private void cmbContinents_Loaded(object sender, RoutedEventArgs e)
        {
           
            GetContinent(); 
            cmbContinents.DisplayMemberPath = "ContinentName";

        }



        // when different values of ComboboX are selected(i.e., continents) This method changes the contnets of listBox. 
        private void cmbContinents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // storing current index (+1) because combobox starts at 0.
            int id = cmbContinents.SelectedIndex + 1;

            SqlData.tblCountries = SqlData.adpCountries.GetById(id);

            if(SqlData.tblCountries.Count > 0)
            {

                lstCountries.ItemsSource = SqlData.tblCountries;
                lstCountries.DisplayMemberPath = "CountryName";

            }


        }

        // this changes the cities according to the country selected in the listbox. 
        private void lstCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            // 1. store current selected list Item as string
            String countryName = "";

            int a = lstCountries.SelectedIndex;

           if(a != -1)
            {
                DataRowView drv = (DataRowView)lstCountries.SelectedItem; // error while changing continent

                countryName = drv["CountryId"].ToString();
                


                // 2. country id of selected item
                int countryId = Convert.ToInt32(countryName);

                SqlData.tblCities = SqlData.adpCities.GetByCityName(countryId);

                if (SqlData.tblCities.Count > 0)
                {

                    grdCities.ItemsSource = SqlData.tblCities;
                    SqlData.tblCountries = SqlData.adpCountries.GetCountries();
                    var row = SqlData.tblCountries[countryId - 1];
                    lblLang.Content = row.Language.ToString();
                    lblCurrency.Content = row.Currency.ToString();

                }
            } else
            {
                cmbContinents_SelectionChanged(sender, e);
            }

               
            }

            // btn to go to new Child Window to add Continent.
            private void btnAddContinents_Click(object sender, RoutedEventArgs e)
        {
      
            AddContinent continent = new AddContinent();
            continent.Closed += new EventHandler(RefreshData);
            continent.Show();
    
        }
        
        // EventHandler primarily made to refresh datatables for this child Window.
        private void RefreshData(object sender, EventArgs e)
        {

            GetContinent();
            cmbContinents.DisplayMemberPath = "ContinentName";

        }

        // btn to go to new Child Window to add Country.
        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
          
            AddCountry country = new AddCountry();
            country.Closed += new EventHandler(RefreshData3);
            country.Show();
           
        }

        // EventHandler primarily made to refresh datatables for this child Window.
        private void RefreshData3(object sender, EventArgs e)
        {
            GetCountry();
        }

        //btn to go to new Child Window to add City.
        private void btnAddCities_Click(object sender, RoutedEventArgs e)
        {

            AddCity city = new AddCity();
            city.Closed += new EventHandler(RefreshData2);
            city.Show();
            
        }

        //EventHandler primarily made to refresh datatables after City Window is altered.
        private void RefreshData2(object sender, EventArgs e)
        {
            GetCity();

        }

    }
}
