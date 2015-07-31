using LooFinder.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using System.Windows.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.Devices.Geolocation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LooFinder
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ParseHelper parseHelper;
        private MapControl _detailMap;
        private MapIcon lastMapPin;
        private MapControl detailMap
        {
            get
            {
                return _detailMap;
            }
            set
            {
                _detailMap = value;
                if(LoosNearbyList.SelectedItem != null)
                {
                    SetToiletMap(LoosNearbyList.SelectedItem);
                }
            }
        }

        public MainPage()
        {
            this.InitializeComponent();
            parseHelper = ParseHelper.Instance;
            getDataCopyrightMessage();

            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            if(parseHelper.parseToilets.Count < 1)
            {
                //findNearbyToilets();
            } else
            {
                LoosNearbyList.ItemsSource = parseHelper.parseToilets;
            }

            CheckForGeolocation();

        }

        private async void CheckForGeolocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();

            switch(accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    await ReverseGeocodedSearch();
                    GetGeocodedSearch();
                    break;
                case GeolocationAccessStatus.Denied:
                    //TODO: Show a message asking if the user would like to enable it, and prompting to
                    break;
                case GeolocationAccessStatus.Unspecified:
                    //TODO: Show error
                    break;
            }
        }

        private async void findNearbyToilets(ParseGeoPoint location)
        {
            //remove existing loos from different location 
            if (parseHelper.parseToilets.Count > 0)
            {
                parseHelper.parseToilets.RemoveRange(0, parseHelper.parseToilets.Count - 1);
            }

            var options = SearchOptions();

            await parseHelper.getNearbyToilets(location, options);

            LoosNearbyList.ItemsSource = parseHelper.parseToilets;
        }

        private Dictionary<String, bool> SearchOptions()
        {
            var options = new Dictionary<string, bool>();
            var maleRequired = false;
            var femaleRequired = false;
            var unisexOK = false;

            if(UnisexCheckbox.IsChecked == true)
            {
                options.Add("Unisex", true);
                unisexOK = true;
            }

            if(MaleCheckbox.IsChecked == true)
            {
                options.Add("Male", true);
                maleRequired = true;
            } 

            if (FemaleCheckbox.IsChecked == true)
            {
                options.Add("Female", true);
                femaleRequired = true;
            }

            if (AccessibleCheckbox.IsChecked == true)
            {
                if (maleRequired)
                {
                    options.Add("AccessibleMale", true);
                } 

                if(femaleRequired)
                {
                    options.Add("AccessibleFemale", true);
                }

                if (unisexOK)
                {
                    options.Add("AccessibleUnisex", true);
                }

            }

            return options;
        }

        private async void getToilet()
        {
            ParseQuery<ParseObject> query = ParseObject.GetQuery("Toilet");
            ParseObject toilet = await query.GetAsync("chPrYKjro9");
            String toiletName = toilet.Get<String>("Name");
            System.Diagnostics.Debug.WriteLine(toiletName);
        }

        private async void getDataCopyrightMessage()
        {
            var dataCopyrightNotice = await parseHelper.getParseConfigElement("DataCopyrightNotice");
            DataCopyrightMessage.Text = dataCopyrightNotice;
        }

        private async void GetGeocodedSearch()
        {
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(
                LocationSearchbox.Text,
                null,
                1
                );

            if(result.Status == MapLocationFinderStatus.Success)
            {
                ParseGeoPoint location = new ParseGeoPoint(result.Locations[0].Point.Position.Latitude, result.Locations[0].Point.Position.Longitude);
                findNearbyToilets(location);
            }
        }

        private async Task ReverseGeocodedSearch()
        {
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = 47.643;
            location.Longitude = -122.131;
            Geopoint pointToReverseGeocode = new Geopoint(location);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(
                pointToReverseGeocode
                );

            if (result.Status == MapLocationFinderStatus.Success)
            {
                LocationSearchbox.Text = result.Locations[0].Address.FormattedAddress;
            }

        }

        private void LoosNearbyList_ItemClick(object sender, ItemClickEventArgs e)
        {

            

            LooDetailContentPresenter.Visibility = Visibility.Visible;

            if (detailMap != null)
            {

                SetToiletMap(e.ClickedItem);
                
            } 
           
        }

        private async void MapControl_Loaded(object sender, RoutedEventArgs e)
        {
            //We know this will only be called when a detail item has been clicked
            detailMap = (MapControl)sender;
        }

        private void SetToiletMap(Object clickedItem)
        {
            Toilet selectedLoo = clickedItem as Toilet;

            if (AdaptiveStates.CurrentState == NarrowState)
            {
                Frame.Navigate(typeof(DetailView), selectedLoo.ObjectId);
            }

            if (lastMapPin != null)
            {
                detailMap.MapElements.Remove(lastMapPin);
            }

            

            lastMapPin = selectedLoo.GetMapIcon();

            detailMap.MapElements.Add(lastMapPin);
            detailMap.Center = selectedLoo.Location;
            //lastMapPin = selectedLoo.UpdateToiletMap(detailMap);
            //detailMap.Center = selectedLoo.Location;
           // MapControl.SetLocation(lastMapPin, selectedLoo.Location);
            //detailMap.Children.Add(lastMapPin);
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {

        }

        private void LocationSearchbox_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                GetGeocodedSearch();
            }
        }
    }
}
