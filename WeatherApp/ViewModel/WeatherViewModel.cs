using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;

        public string Query
        {
            get { return query; }
            set { 
                query = value;
                // call method to trigger event that property value has been changed
                OnPropertyChanged("Query");
            }
        }

        public ObservableCollection<City> Cities { get; set; }

        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get { return currentConditions; }
            set { 
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        private City selectedCity;

        public City SelectedCity
        {
            get { return selectedCity; }
            set { 
                selectedCity = value;
                OnPropertyChanged("SelectedCity");
                GetCurrentConditions();
            }
        }

        public SearchCommand SearchCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// method to trigger event of property change
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); 
        }

        public WeatherViewModel()
        {
            // implement default constructor to display default information
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City
                {
                    LocalizedName = "New York"
                };

                CurrentConditions = new CurrentConditions
                {
                    WeatherText = "Partly cloudy",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    },
                    HasPrecipitation = true
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }

        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccWeatherHelper.GetCurrentConditions(SelectedCity.Key);
        }

        public async void MakeQuery()
        {
            var cities = await AccWeatherHelper.GetCities(Query);

            Cities.Clear(); // clear old collection of cities
            
            foreach(var city in cities)
            {
                Cities.Add(city);
            }
        }
    }
}
