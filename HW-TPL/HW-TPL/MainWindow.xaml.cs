using HW_TPL.Models;
using HW_TPL.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace HW_TPL
{
    public partial class MainWindow : Window
    {
        private int indexFilter;
        private int indexType;
        private Deserialize service;
        private List<string> typeNames;
        private string typeName;
        private List<string> drinks;
        private AllDrinks resultDrink;
        private AllDrinks resultAllDrinks;
        private Database database;

        public MainWindow()
        {
            InitializeComponent();

            typeNames = new List<string>();
            drinks = new List<string>();
            service = new Deserialize();
            database = new Database();
        }

        
        public void InitType()
        {
            Filter result;
            if (typeNames.Count > 0)
            {
                typeNames.Clear();
            }
            result = service.ExecuteFilterName(indexFilter);
            switch (indexFilter)
            {
                case 0:
                    foreach (var name in result.FilterNames)
                    {
                        typeNames.Add(name.NameCategory);
                    }
                    break;
                case 1:
                    foreach (var name in result.FilterNames)
                    {
                        typeNames.Add(name.NameAlcohol);
                    }
                    break;
            }
        }
        
        private void FilterSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            indexFilter = filter.SelectedIndex;
            InitTypeAsync();

            type.ItemsSource = typeNames;
            //TODO isCompleted?
            type.IsEnabled = true;
        }
        private async void InitTypeAsync()
        {
            await Task.Run(() => InitType());
        }

        private void TypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            indexType = type.SelectedIndex;
            typeName = type.SelectedItem.ToString();
            
            resultDrink = service.ExecuteAllDrinks(indexFilter, typeName, null);
            listCoctails.ItemsSource = resultDrink.Drinks;
        }

        private void ListCoctailsSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            resultAllDrinks = service.ExecuteAllDrinks(null, null, resultDrink.Drinks.ToArray()[listCoctails.SelectedIndex].IdDrink);
            nameDrink.Text = resultAllDrinks.Drinks.ToArray()[0].NameDrink;
            categoryDrink.Text = resultAllDrinks.Drinks.ToArray()[0].CategoryDrink;
            alcoholDrink.Text = resultAllDrinks.Drinks.ToArray()[0].AlcoholicFortresDrink;
            ingredDrink.Text = resultAllDrinks.Drinks.ToArray()[0].IngredientDrink;
        }

        private void OrderDrink(object sender, RoutedEventArgs e)
        {
            database.Write(resultAllDrinks);
            MessageBox.Show("Order is accepted");
        }
    }
}
