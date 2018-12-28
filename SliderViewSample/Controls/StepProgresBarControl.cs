using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace SliderViewSample.Controls
{
    public class StepProgressBarControl : StackLayout
    {
        Button _lastStepSelected;

        public static readonly BindableProperty StepsProperty =
            BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBarControl), 0);

        public static readonly BindableProperty StepSelectedProperty =
            BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBarControl), 0, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty StepColorProperty =
            BindableProperty.Create(nameof(StepColor), typeof(Color), typeof(StepProgressBarControl), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UnselectedStepColorProperty =
            BindableProperty.Create(nameof(UnselectedStepColor), typeof(Color), typeof(StepProgressBarControl), Color.Transparent, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty DividersEnabledProperty =
            BindableProperty.Create(nameof(DividersEnabled), typeof(bool), typeof(StepProgressBarControl), true, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(StepProgressBarControl), default(IEnumerable<object>),
                            BindingMode.TwoWay, null, ItemsSourceChanged);

        EventHandler<int> _onStepSelected;

        public event EventHandler<int> OnStepSelected
        {
            add => _onStepSelected += value;
            remove => _onStepSelected -= value;
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        static void ItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var itemsLayout = (StepProgressBarControl)bindable;
            IList values = (System.Collections.IList)newValue;
            itemsLayout.Steps = values.Count;
        }

        public Color StepColor
        {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
        }

        public Color UnselectedStepColor
        {
            get { return (Color)GetValue(UnselectedStepColorProperty); }
            set { SetValue(UnselectedStepColorProperty, value); }
        }

        public int Steps
        {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected
        {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }

        public bool DividersEnabled
        {
            get { return (bool)GetValue(DividersEnabledProperty); }
            set { SetValue(DividersEnabledProperty, value); }
        }


        public StepProgressBarControl()
        {
            Orientation = StackOrientation.Horizontal;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Padding = new Thickness(10, 0);
            Spacing = 0;
            AddStyles();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName)
            {
                Children.Clear();

                for (int i = 0; i < Steps; i++)
                {
                    var button = new Button
                    {
                        Text = $"{i + 1}",
                        ClassId = $"{i + 1}",
                        Style = Resources["unSelectedStyle"] as Style
                    };

                    button.Clicked += Handle_Clicked;

                    Children.Add(button);

                    if (DividersEnabled)
                    {
                        if (i < Steps - 1)
                        {
                            var separatorLine = new BoxView
                            {
                                BackgroundColor = Color.Silver,
                                HeightRequest = 1,
                                WidthRequest = 5,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalOptions = LayoutOptions.FillAndExpand
                            };
                            Children.Add(separatorLine);
                        }
                    }

                }
            }
            else if (propertyName == StepSelectedProperty.PropertyName)
            {
                var children = Children.First(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == StepSelected));
                if (children != null) SelectElement(children as Button);

            }
            else if (propertyName == StepColorProperty.PropertyName || propertyName == UnselectedStepColorProperty.PropertyName)
            {
                AddStyles();
            }
            else if (propertyName == DividersEnabledProperty.PropertyName)
            {
                if (!DividersEnabled)
                {
                    Spacing = 16;
                    HorizontalOptions = LayoutOptions.CenterAndExpand;
                }
            }
        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            Button elementSelected = sender as Button;

            SelectElement(elementSelected);

            StepSelected = Convert.ToInt32(elementSelected.Text);
            _onStepSelected?.Invoke(this, StepSelected);

        }

        void SelectElement(Button elementSelected)
        {
            if (Steps == 0) return;

            if (_lastStepSelected != null) _lastStepSelected.Style = Resources["unSelectedStyle"] as Style;

            elementSelected.Style = Resources["selectedStyle"] as Style;

            StepSelected = Convert.ToInt32(elementSelected.Text);

            _lastStepSelected = elementSelected;
        }

        void AddStyles()
        {
            var unselectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = UnselectedStepColor },
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = UnselectedStepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 6 },
                    new Setter { Property = HeightRequestProperty,   Value = 12 },
                    new Setter { Property = WidthRequestProperty,   Value = 12 }
            }
            };

            var selectedStyle = new Style(typeof(Button))
            {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.TextColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 6 },
                    new Setter { Property = HeightRequestProperty,   Value = 12 },
                    new Setter { Property = WidthRequestProperty,   Value = 12 },
                    new Setter { Property = Button.FontAttributesProperty,   Value = FontAttributes.Bold }
            }
            };

            Resources = new ResourceDictionary();
            Resources.Add("unSelectedStyle", unselectedStyle);
            Resources.Add("selectedStyle", selectedStyle);
        }
    }
}
