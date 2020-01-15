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
        public CalculateFormula()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private void EditButton_Clicked(object sender, EventArgs e)
        {

        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {

        }

        public void BTNAdd_Clicked(object sender, EventArgs e)
        {
            try
            {

                var product = new Product
                {
                    Name = TBName.Text,
                    RatePerKg = float.Parse(TBRate.Text),
                    Weight = float.Parse(TBWeight.Text),
                    SrNo = ProductList.Count + 1
                };
                product.Total = product.RatePerKg * product.Weight;
                ProductList.Add(product);
                refresh();
            }
            catch(Exception ex)
            {
                DisplayAlert(ex.Message,"ok","cancel");
            }
        }
        private void refresh()
        {
            TBName.Text = "";
            TBRate.Text = "";
            TBWeight.Text = "";
        }
    }
}