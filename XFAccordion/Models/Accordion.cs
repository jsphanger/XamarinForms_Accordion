using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

using XFAccordion.Models.LiquorStore;

namespace XFAccordion.Models
{
    public class AccordionView : ScrollView
    {
        private StackLayout _mainLayout = new StackLayout() { Spacing = 0 };

        private List<Liquor> _itemSource;
        public List<Liquor> ItemSource { get { return _itemSource; } set { _itemSource = value; OnPropertyChanged(nameof(ItemSource)); } }

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(propertyName: "ItemSource", returnType: typeof(IList), declaringType: typeof(AccordionView), defaultValue: null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: ItemSourceChanged);

        public AccordionView() { }

        private void GenerateMainLayout()
        {
            /* GOAL: Build the main layout to contain x amount of section objects
             *       which will contain two items (Header and Body)
             */

            _mainLayout.Children.Clear();


            //-- Q: How do you group by on a generic list?
            //-- A: You don't.  Have the use send over there already grouped list!
            
            LiquorType lastGroup = new LiquorType();
            var source = (List<Liquor>)ItemSource;

            foreach (var item in source)
            {
                if (lastGroup != item.Type)
                {
                    //set new liquor type
                    lastGroup = item.Type;

                    var bindableDataGroup = source.Where(x => x.Type == lastGroup).ToList();

                    _mainLayout.Children.Add(new AccordionSection(bindableDataGroup));
                }
            }

            /*
            foreach (var item in ItemSource)
                _mainLayout.Children.Add(new AccordionSection(ItemSource));
            */

            //add the main layout to this scrollview
            Content = _mainLayout;
        }

        private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue == newValue)
                return;

            if (newValue != null)
            {
                var control = (AccordionView)bindable;
                control.ItemSource = (List<Liquor>)newValue;
                //control.ItemSource = (IList)newValue;
                control.GenerateMainLayout();
            }
        }
    }

    //Bindable Layout Child
    public class AccordionSection : StackLayout
    {
        private StackLayout _header = new StackLayout();
        private StackLayout _body = new StackLayout();

        public bool IsOpen { get; private set; }

        public AccordionSection(IList list)
        {
            //Close any gaps in this parent container
            Padding = 0;
            Margin = 0;
            Spacing = 0;

            _body.Spacing = 0;
            _body.HeightRequest = 0;
            _body.IsVisible = false;
            
            Children.Clear();

            //Build the header
            BindableLayout.SetItemsSource(_header, new[] { list[0] });
            BindableLayout.SetItemTemplate(_header, new DataTemplate(typeof(SectionHeaderView)));

            //Bind the body
            BindableLayout.SetItemsSource(_body, list);
            BindableLayout.SetItemTemplate(_body, new DataTemplate(typeof(SectionBodyView)));

            Children.Add(_header);
            Children.Add(_body);

            _header.GestureRecognizers.Add( new TapGestureRecognizer {

                    Command = new Command(async () =>
                    {
                        var view = (SectionHeaderView)_header.Children[0];
                        var image = view.Children.Where(x => x.GetType() == typeof(Image)).FirstOrDefault();
                        uint animationSpeed = 150;

                        if (IsOpen)
                        {
                            image.RotateTo(0, animationSpeed);
                            var animation = new Animation(v => _body.HeightRequest = v, Application.Current.MainPage.HeightRequest, 0, Easing.SpringIn);
                            animation.Commit(_body, "Closing", length: animationSpeed);
                            _body.IsVisible = false;
                            IsOpen = false;
                        }
                        else
                        {
                            image.RotateTo(45, animationSpeed);
                            var bodyAnimation = new Animation(v => _body.HeightRequest = v, 0, Application.Current.MainPage.HeightRequest, Easing.SpringOut);
                            bodyAnimation.Commit(_body, "Opening", length: animationSpeed);
                            _body.IsVisible = true;
                            IsOpen = true;
                        }
                    })
                }
            );
        }
    }

    //DataTemplates - provided by end user
    public class SectionHeaderView : StackLayout
    {
        private Label _title = new Label();
        private Image _image = new Image() { ClassId = "headerimage" };

        public SectionHeaderView() {

            Orientation = StackOrientation.Horizontal;
            Margin = new Thickness(0,10,0,0);
            Padding = new Thickness(0, 10);
            BackgroundColor = Color.LightBlue;
            Spacing = 0;

            _title.SetBinding(Label.TextProperty, "Type");
            //_title.SetBinding(Label.TextProperty, "GroupTitle");
            _title.FontSize = 18;
            _title.FontAttributes = FontAttributes.Bold;
            _title.HorizontalTextAlignment = TextAlignment.Center;

            _image.Source = "openicon";
            _image.HorizontalOptions = LayoutOptions.EndAndExpand;
            _image.Margin = new Thickness(5);
            _image.HeightRequest = 24;

            Children.Add(_title);
            Children.Add(_image);
        }
    }
    public class SectionBodyView : Grid
    {
        private Label _brand = new Label(),
                      _size = new Label(),
                      _price = new Label(),
                      _qty = new Label();

        public SectionBodyView()
        {
            BackgroundColor = Color.WhiteSmoke;
            
            ColumnSpacing = 2;
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
            ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });

            RowDefinitions.Add(new RowDefinition() { Height = new GridLength(30, GridUnitType.Star) });

            _brand.SetBinding(Label.TextProperty, "BrandName");
            _size.SetBinding(Label.TextProperty, "Size");
            _price.SetBinding(Label.TextProperty, "Price");
            _qty.SetBinding(Label.TextProperty, "Quantity");

            Children.Add(_brand, 0, 0);
            Children.Add(_size, 1, 0);
            Children.Add(_price, 2, 0);
            Children.Add(_qty, 3, 0);
        }
    }
}
