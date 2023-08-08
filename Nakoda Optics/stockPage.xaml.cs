using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace Nakoda_Optics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class stockPage : ContentPage
    {
        public Command TouchCommand { get; set; }
        public stockPage()
        {
            InitializeComponent();

            //TouchCommand = new Command(() =>
            //{
            //    DisplayAlert("hwllo", "hi", "ok");
            //});

            //BindingContext = this;
            showall();

            mystocklist.RefreshCommand = new Command(() =>
            {
                mystocklist.IsRefreshing = true;
                showall();
                mystocklist.IsRefreshing = false;

            });

        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new addPage());
        }


        public void showall()
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);

            con.CreateTable<StockClass>();

            var mystock = con.Query<StockClass>("SELECT * FROM StockClass");
            mystocklist.ItemsSource = mystock;


            con.Close();

        }

        private void stocksearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            mystocklist.ItemsSource = con.Table<StockClass>().Where(x => x.Compony.StartsWith(e.NewTextValue));
        }

        private async void mystocklist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = (StockClass)e.SelectedItem;
            var temp = await DisplayActionSheet("SELECT", "Cancle", "", $"H! its, { selected.Compony}'s {selected.Model}" , "DELETE");



           

            if (temp == "DELETE")
            {

                var t = selected.Compony + selected.Model + selected.Color;

                SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
                con.CreateTable<StockClass>();
                var data4 = con.Query<StockClass>($"DELETE FROM StockClass WHERE Id =='{t}'");



            }

           
        }
    }
}