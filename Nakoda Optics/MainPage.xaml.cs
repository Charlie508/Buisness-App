using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nakoda_Optics
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Stock_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new stockPage());

        }

        private void Order_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new trackorderPage());
        }
    }
}
