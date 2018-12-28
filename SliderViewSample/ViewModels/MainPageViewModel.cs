using System;
using System.Collections.Generic;
using System.ComponentModel;
using SliderViewSample.Models;

namespace SliderViewSample.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public IList<SliderViewItem> Data { get; set; } = new List<SliderViewItem>()
        {
            new SliderViewItem()
            {
                Title = "Users",
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                Icon = "ic_account_white.png",
                Step = 1,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#00BCD4")
            },
            new SliderViewItem()
            {
                Title = "Favorites",
                Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ",
                Icon = "ic_heart_white.png",
                Step = 2,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#E91E63")
            },
            new SliderViewItem()
            {
                Title = "Messages",
                Description = "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.",
                Icon = "ic_inbox_white.png",
                Step = 3,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#00BCD4")
            },
            new SliderViewItem()
            {
                Title = "Events",
                Description = "Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                Icon = "ic_event_white.png",
                Step = 4,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#E91E63")
            },
            new SliderViewItem()
            {
                Title = "Explore",
                Description = "Tempus iaculis urna id volutpat. Eget gravida cum sociis natoque penatibus et.",
                Icon = "ic_explore_white.png",
                Step = 5,
                BackgroundColor = Xamarin.Forms.Color.FromHex("#00BCD4")
            }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public MainPageViewModel()
        {
        }
    }
}
