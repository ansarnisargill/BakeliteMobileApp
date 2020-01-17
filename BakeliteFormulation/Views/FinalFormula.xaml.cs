using BakeliteFormulation.Helpers;
using BakeliteFormulation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace BakeliteFormulation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalFormula : ContentPage
    {
        public List<Product> Products { get; set; }
        public FinalFormula(List<Product> products)
        {
            InitializeComponent();
            Products = products;
        }

        private void ContentPage_Appearing(object sender, EventArgs e)
        {
            FontHelper.SetUrduFont(new List<Label>() { lblkaat, lbltotalPrice, lblTotalWeight, lblAkhrajaat, lblFinalWazan, lblFinalRaqam, lblFiKiloRate, lblBagSize, lblBagRate });
            FinalWeight.Text = TotalBakeliteWeight.Text = this.Products.Sum(x => x.Weight).ToString() + " KG";
            FinalPrice.Text = TotalPrice.Text = this.Products.Sum(x => x.Total).ToString() + " Rs.";
            CalculatePerKGRate();
            CalculatePerBagPrice();
        }

        private void TBKaat_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!float.TryParse(TBKaat.Text, out float kaat))
            {
                kaat = 0;
            }
            FinalWeight.Text = (this.Products.Sum(x => x.Weight) - kaat).ToString() + " KG";
            CalculatePerKGRate();
            CalculatePerBagPrice();
        }

        private void TBElectricity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!float.TryParse(TBElectricity.Text, out float bills))
            {
                bills = 0;
            }
            FinalPrice.Text = (this.Products.Sum(x => x.Total) + bills).ToString() + " Rs.";
            CalculatePerKGRate();
            CalculatePerBagPrice();

        }
        private void CalculatePerKGRate()
        {
            var FinalP = float.Parse(FinalPrice.Text.Split(' ')[0]);
            var FinalW = float.Parse(FinalWeight.Text.Split(' ')[0]);
            PerKGPrice.Text = Math.Round((FinalP / FinalW), 2).ToString() + " Rs.";
           
        }
        private void CalculatePerBagPrice()
        {
            if (!float.TryParse(TBBagSize.Text, out float BagSize))
            {
                BagSize = 0;
            }
            FinalBagRate.Text = Math.Round((float.Parse(PerKGPrice.Text.Split(' ')[0]) * BagSize), 2).ToString() + " Rs.";
        }

        private void TBBagSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            CalculatePerBagPrice();
        }

        private async void BTNClear_Clicked(object sender, EventArgs e)
        {
            Flags.ClearGrid = true;
           await Navigation.PopAsync();
        }
    }
}