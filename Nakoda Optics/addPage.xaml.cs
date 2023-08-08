using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nakoda_Optics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class addPage : ContentPage
    {
        public addPage()
        {
            InitializeComponent();

        }


        private async void savebtn_Clicked(object sender, EventArgs e)
        {
            if (brand.Text != null && modell.Text != null && quantity.Text != null && colorpicker.Text != null)
            {
                var cpv = colorpicker.Text;
                var cp = brand.Text + modell.Text + cpv;
                SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
                con.CreateTable<StockClass>();
                con.CreateTable<color>();
                var data2 = con.Query<color>($" SELECT * FROM color WHERE COLORS == '{cpv}'");
                if (data2.Count < 1)
                {
                    color c = new color()
                    {
                        COLORS = colorpicker.Text,
                    };

                    var data3 = con.Insert(c);
                }


                var dataa = con.Query<StockClass>($"SELECT * FROM StockClass WHERE Id == '{cp}'");
                if (dataa.Count > 0)
                {
                    //DisplayAlert("Error", "Stock Alredy added , add another one ", "ok");

                    var options = new ToastOptions
                    {
                        BackgroundColor = Color.Red,
                        Duration = TimeSpan.FromSeconds(2),

                        MessageOptions = new MessageOptions
                        {
                            Foreground = Color.White,
                            Font = Font.SystemFontOfSize(16),
                            Message = "                  Stock is alredy added !",

                        }
                    };
                    await this.DisplayToastAsync(options);

                }

                else
                {

                    StockClass st = new StockClass()
                    {
                        Compony = brand.Text,
                        Model = modell.Text,
                        Color = colorpicker.Text,
                        Quantity = quantity.Text.ToString(),
                        Id = cp,

                    };

                    var data = con.Insert(st);
                    con.Close();
                    DisplayAlert("Great", "New Record Added Successfully !", "ok");
                }

            }

            else
            {
                DisplayAlert("Opps", "Complete All Fields", "ok");
            }

        }

        public void getalll()
        {
            colorlist.IsVisible = true;
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            con.CreateTable<color>();
            var mystock1 = con.Query<color>("SELECT * FROM color");

            colorlist.ItemsSource = mystock1;
            con.Close();



        }

        private void colorpicker_TextChanged(object sender, TextChangedEventArgs e)
        {

            getalll();
            colorlist.IsVisible = true;
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            colorlist.ItemsSource = con.Table<color>().Where(x => x.COLORS.StartsWith(e.NewTextValue));
            con.Close();


        }

        private void colorlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selectedcol = (color)e.SelectedItem;
            colorpicker.Text = selectedcol.COLORS;
            colorlist.IsVisible = false;
        }
    }
}