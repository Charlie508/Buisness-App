using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nakoda_Optics
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class detailPage : ContentPage
    {
        string myname, mydate,mynum,mycity;
        public detailPage(string name , string date , string num,string city)
        {
            InitializeComponent();
            tracklist.RefreshCommand = new Command(() =>
            {
                tracklist.IsRefreshing = true;
                getsall();
                tracklist.IsRefreshing = false;
                

            });

            myname = name;
            mydate = date;
            mynum = num;
            mycity = city;

            customername.Text = myname;
            thedate.Text = date;
            custcity.Text = city;
            customercon.Text = num;

        }

        public void getsall()
        {
            SQLiteConnection con = new SQLiteConnection(App.LocalDatabase);
            con.CreateTable<OrderHistory0>();
            var mystock1 = con.Query<OrderHistory0>($"select * from OrderHistory0 where custname == '{myname}' AND Date == '{mydate}'");
            tracklist.ItemsSource= mystock1;

            var total = con.Query<OrderHistory0>($"select sum(thequantity) from OrderHistory0 where custname == '{myname}' AND Date == '{mydate}'");

           
            

            
           

            con.Close();


            con.Close();
        }






    }
}