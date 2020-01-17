using BakeliteFormulation.Helpers;
using BakeliteFormulation.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakeliteFormulation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalculateFormula : ContentPage
    {
        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();
        private readonly string ProductNamePrefix = "Product";
        public CalculateFormula()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var product = dg.SelectedItem as Product;
                TBName.Text = product.Name;
                TBRate.Text = product.RatePerKg.ToString();
                TBWeight.Text = product.Weight.ToString();
                DeleteItemFromList(product);
                CalculateNameAndSrno();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error",ex.Message,"Ok");
            }
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                var product = dg.SelectedItem as Product;
                DeleteItemFromList(product);
                CalculateNameAndSrno();

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }
        }
        private void DeleteItemFromList(Product p)
        {
            this.ProductList.Remove(p);
        }

        public void BTNAdd_Clicked(object sender, EventArgs e)
        {
            try
            {
                if(!float.TryParse(TBRate.Text,out float ratePerKg))
                {
                    DisplayAlert("Error", "Invalid rate KG", "Ok");
                    return;
                }
                if (!float.TryParse(TBWeight.Text, out float weight))
                {
                    DisplayAlert("Error", "Invalid Weight", "Ok");
                    return;
                }
                var name = TBName.Text; 
                if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
                {
                    name= this.ProductNamePrefix;
                }
                var product = new Product
                {
                    Name = name,
                    RatePerKg = ratePerKg,
                    Weight = weight,
                    SrNo =  1
                };
                product.Total = product.RatePerKg * product.Weight;
                ProductList.Add(product);
                CalculateNameAndSrno();
                refresh();
            }
            catch(Exception ex)
            {
                DisplayAlert("Error",ex.Message,"Ok");
            }
        }
        private void CalculateNameAndSrno()
        {
            var k = 1;
            foreach(var product in this.ProductList)
            {
                product.SrNo = k;
                if (product.Name.Contains(this.ProductNamePrefix))
                {
                    product.Name = this.ProductNamePrefix + k;
                }
                k++;

            }
        }
        private void refresh()
        {
            TBName.Text = "";
            TBRate.Text = "";
            TBWeight.Text = "";
        }

        async void BTNNext_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FinalFormula(this.ProductList.ToList()));
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            if (Flags.ClearGrid)
            {
                foreach(var p in this.ProductList.ToList())
                {
                    DeleteItemFromList(p);
                }
                Flags.ClearGrid = false;
            }
        }
    }
}