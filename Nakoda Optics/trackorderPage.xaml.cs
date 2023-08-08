using SQLite;
using System;
using System.Text;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Nakoda_Optics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class trackorderPage : ContentPage
    {
        public trackorderPage()
        {
            InitializeComponent();
            getsall();
          
            orderlist.RefreshCommand = new Command(() =>
            {
                orderlist.IsRefreshing = true;
                getsall();
                orderlist.IsRefreshing = false;

            });
            
        }


        public void getsall()
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            con.CreateTable<OrderHistory0>();
            var mystock1 = con.Query<OrderHistory0>("select * from OrderHistory0 ");
            orderlist.ItemsSource = mystock1;
            con.Close();
        }



        private void neworder_Clicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new orderPage());
        }

        private void orderlist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selecteditem = (OrderHistory0)e.SelectedItem;

            Navigation.PushAsync(new detailPage(selecteditem.custname,selecteditem.Date , selecteditem.contact,selecteditem.city));
            
             
        }

        [Obsolete]
        private void Button_Clicked(object sender, EventArgs e)
        {
           
        }

        private void ordersearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            orderlist.ItemsSource = con.Table<OrderHistory0>().Where(x => x.custname.StartsWith(e.NewTextValue));

        }
    }
}