using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SliderViewSample.Models;
using SliderViewSample.ViewModels;
using Xamarin.Forms;

namespace SliderViewSample
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel Context;

        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();
        }
    }
}
