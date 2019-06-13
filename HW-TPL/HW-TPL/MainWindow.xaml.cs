using HW_TPL.DTO;
using HW_TPL.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace HW_TPL
{
    public partial class MainWindow : Window
    {
        private int indexFilter;
        private Deserialize service;
        private ObservableCollection<string> typeNames;
        private string typeName;
        private List<string> drinks;
        private AllDrinks resultDrink;
        private AllDrinks resultAllDrinks;
        private Database database;

        public MainWindow()
        {
            InitializeComponent();
            typeNames = new ObservableCollection<string>();
            drinks = new List<string>();
            service = new Deserialize();
            database = new Database();
            Task.Run(()=> database.ReportOrders());
        }


        public void InitType()
        {
            Filter result;
            if (typeNames.Count > 0)
            {
                Dispatcher.Invoke(() => typeNames.Clear());
                Dispatcher.Invoke(() => type.IsEnabled = false);
            }
            //result = service.ExecuteFilterName(indexFilter);
            var taskType = Task.Run(() =>
            {
                return service.ExecuteFilterName(indexFilter);
                 
            }).Result;

            result = taskType;
            switch (indexFilter)
            {
                case 0:
                    foreach (var name in result.FilterNames)
                    {
                        Dispatcher.Invoke(() => typeNames.Add(name.NameCategory));
                    }
                    break;
                case 1:
                    foreach (var name in result.FilterNames)
                    {
                        Dispatcher.Invoke(() => typeNames.Add(name.NameAlcohol));
                    }
                    break;
            }
            Dispatcher.Invoke(() => type.IsEnabled = true);
        }

        private void FilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            indexFilter = filter.SelectedIndex;
            InitTypeAsync();
            type.ItemsSource = typeNames;
        }
        private async void InitTypeAsync()
        {
            await Task.Run(() => InitType());
        }

        private void TypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (type.SelectedItem != null)
            {
                typeName = type.SelectedItem.ToString();
                //resultDrink = service.ExecuteAllDrinks(indexFilter, typeName, null);
                var taskDrink = Task.Run(() =>
                {
                    return service.ExecuteAllDrinks(indexFilter, typeName, null);

                }).Result;

                resultDrink = taskDrink;
                listCoctails.ItemsSource = resultDrink.Drinks;
            }
            else
            {
                typeName = "";
            }
        }

        private void ListCoctailsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listCoctails.SelectedIndex != -1)
            {
                resultAllDrinks = service.ExecuteAllDrinks(null, null, resultDrink.Drinks.ToArray()[listCoctails.SelectedIndex].IdDrink);
                coctailsInfo.ItemsSource = resultAllDrinks.Drinks;
                count.IsEnabled = true;
                image.Source = ImageInit(resultAllDrinks.Drinks.ToArray()[0].PhotoDrink);
            }
            else
            {
                count.IsEnabled = false;
                order.IsEnabled = false;
                coctailsInfo.ItemsSource = null;
            }
        }

        public BitmapImage ImageInit(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            return bitmap;
        }

        private void CountSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            order.IsEnabled = true;
        }
        private void OrderDrink(object sender, RoutedEventArgs e)
        {
            resultAllDrinks.Drinks.ToArray()[0].CountDrink = count.SelectedIndex + 1;
            Task.Run(()=> database.Write(resultAllDrinks));
            MessageBox.Show("Order is accepted");
        }

    }
}
