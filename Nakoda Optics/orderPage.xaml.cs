using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nakoda_Optics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class orderPage : ContentPage
    {
        public orderPage()
        {
            InitializeComponent();
        }

        public void getall()
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            con.CreateTable<StockClass>();
            var mystock = con.Query<StockClass>("SELECT * FROM StockClass");
            mylist.ItemsSource = mystock;
            con.Close();
        }

        private void model_TextChanged(object sender, TextChangedEventArgs e)
        {

            getall();
            deletbtn.IsVisible = true;
            orderQuantity.IsVisible = true;
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            mylist.ItemsSource = con.Table<StockClass>().Where(x => x.Model.StartsWith(e.NewTextValue)) ;
            con.Close();

        }



        private void mylist_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = (StockClass)e.SelectedItem;

            mylabel.Text = selected.Model;
            int max = Int32.Parse(selected.Quantity);

            mylist.IsVisible = false;
            componyselected.Text = selected.Compony;
            mylabel.IsVisible = true;

           
            


            Application.Current.Properties["qty"] = selected.Quantity;
            Application.Current.Properties["mod"] = selected.Model;
            Application.Current.Properties["col"] = selected.Color;

                    

        }

        private void orderQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {


            if (orderQuantity.Text != string.Empty)
            {
                var x = Application.Current.Properties["qty"].ToString();

                int z = Int32.Parse(x);
                int a = Int32.Parse(orderQuantity.Text);
               

                
                if (a > z )
                {
                    DisplayAlert("oops", $"You Have {z} Pieces Left Only !!", "ok");
                    orderQuantity.Text = string.Empty;
                                      
                }

            }
        }

        private void deletbtn_Clicked(object sender, EventArgs e)
        {
            mylabel.Text = string.Empty;
            componyselected.Text = string.Empty;
            orderQuantity.Text = string.Empty;
            
            mylist.IsVisible = true;

        }

        private void recorddata_Clicked(object sender, EventArgs e)
        {
            if (ccity.Text != null && cname.Text != null && cmobile.Text != null &&
                orderQuantity.Text != string.Empty && mylabel.Text != string.Empty && componyselected.Text != string.Empty )
            {

                var temp = Application.Current.Properties["col"].ToString();
                var cpp = mylabel.Text + temp;

                var myid = componyselected.Text + cpp  ; 

                DateTime d = DateTime.Now;
               
                SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
                con.CreateTable<OrderHistory0>();

                OrderHistory0 od = new OrderHistory0()
                {
                    id = cpp,
                    color = temp,
                    city = ccity.Text,
                    custname = cname.Text,
                    contact = cmobile.Text,
                    compony = componyselected.Text,
                    thequantity = Int32.Parse(orderQuantity.Text),
                    model = mylabel.Text,
                    price = float.Parse(pricebtn.Text),                  
                    Date = d.ToString("dd MMMM yyyy"),
                    Time = d.ToString(" hh : mm ")

            };
                var data = con.Insert(od);

                var oq = orderQuantity.Text;
                
               
                con.CreateTable<StockClass>();
                
                con.Query<StockClass>($"UPDATE StockClass    SET Quantity= Quantity-{oq}   WHERE Id=='{myid}'");


                con.Close();
               

                DisplayAlert("Great", "Order Placed Successfully !", "ok");
              
               
            }
            else
            {
                DisplayAlert("Opps", "Complete All Fields", "ok");
            }
        }
    }
}