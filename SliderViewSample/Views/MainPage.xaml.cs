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

            MainCarousel.ItemSelected += (sender, args) =>
            {
                if (!(args.SelectedItem is SliderViewItem item))
                    return;

                if (CarouselStepBar.Steps > 0)
                    CarouselStepBar.StepSelected = (int)item.Step;
            };

            CarouselStepBar.OnStepSelected += (sender, args) =>
            {
                if (!(args is int step))
                    return;
                var value = step;
                MainCarousel.Position = --value;
            };
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Context = this.BindingContext as MainPageViewModel;

            if(Context.Data.Count > 0)
            {
                CarouselStepBar.StepSelected = 1;
            }
        }
    }
}
