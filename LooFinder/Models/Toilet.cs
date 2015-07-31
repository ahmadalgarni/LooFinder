using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parse;
using Windows.Devices.Geolocation;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls.Maps;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI;

namespace LooFinder.Models
{
    [ParseClassName("Toilet")]
    public class Toilet : ParseObject
    {
        [ParseFieldName("attribute")]
        public ParseClassNameAttribute parseClassNameAttribute {
            get
            {
                return parseClassNameAttribute;
            } set
            {
                parseClassNameAttribute = new ParseClassNameAttribute("Toilet");
            }
        }

        private ParseGeoPoint _searchLocationPoint;
        public ParseGeoPoint searchLocationPoint {get {
                return _searchLocationPoint;
            }
            set
            {
                _searchLocationPoint = value;
                this.distanceFromSearchPoint = 0.0;
            }
        }
        private double _distanceFromSearchPoint;
        public double distanceFromSearchPoint
        {
            get 
            {
                return _distanceFromSearchPoint;
            }
            set
            {
                ParseGeoPoint locationFrom = new ParseGeoPoint(this.Latitude, this.Longitude);
                var distanceTo = locationFrom.DistanceTo(searchLocationPoint);
                _distanceFromSearchPoint = distanceTo.Kilometers;
                //TODO: Allow the user to return distance in miles
            }
        }

        public Toilet() {
        }

        public ObservableCollection<MapIcon> MapItems { get; set; }

        public Geopoint Location
        {
            get
            {
            
                var location = new Geopoint(new BasicGeoposition() {
                    Latitude = this.Latitude,
                    Longitude = this.Longitude
                });

                return location;
                
            }
        }

        [ParseFieldName("Name")]
        public string ToiletName
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("AccessLimited")]
        public bool AccessLimited
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("AccessNote")]
        public string AccessNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("AccessibleFemale")]
        public bool AccessibleFemale
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("AccessbileMale")]
        public bool AccessibleMale
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("AccessibleNote")]
        public string AccessibleNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("AccessibleParkingNote")]
        public string AccessibleParkingNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("AccessibleUnisex")]
        public bool AccessibleUnisex
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("AddressNote")]
        public string AddressNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("BabyChange")]
        public bool BabyChange
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("DrinkingWater")]
        public bool DrinkingWater
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("FacilityType")]
        public string FacilityType
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Female")]
        public bool Female
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("IconAltText")]
        public string IconAltText
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("IconURL")]
        public string IconURL
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("IsOpen")]
        public string IsOpen
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("KeyRequired")]
        public bool KeyRequired
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("MLAK")]
        public bool MLAK
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("Male")]
        public bool Male
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("Notes")]
        public string Notes
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("OpeningHoursNote")]
        public string OpeningHoursNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("OpeningHoursSchedule")]
        public string OpeningHoursSchedule
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Parking")]
        public bool Parking
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("ParkingAccessible")]
        public bool ParkingAccessible
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("ParkingNote")]
        public string ParkingNote
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("PaymentRequired")]
        public bool PaymentRequired
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("Postcode")]
        public int Postcode
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("SanitaryDisposal")]
        public bool SanitaryDisposal
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("SharpsDisposal")]
        public bool SharpsDisposal
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("Showers")]
        public bool Showers
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("State")]
        public string ToiletState
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Status")]
        public string Status
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("ToiletID")]
        public int ToiletID
        {
            get { return GetProperty<int>(); }
            set { SetProperty<int>(value); }
        }

        [ParseFieldName("ToiletType")]
        public string ToiletType
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Town")]
        public string Town
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("URL")]
        public string URL
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Unisex")]
        public bool Unisex
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }

        [ParseFieldName("Address1")]
        public string ToiletAddress
        {
            get {
                var address = GetProperty<string>();
                if(address == null)
                {
                    return "No address provided";
                } else
                {
                    return address;
                }
            }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("Latitude")]
        public Double Latitude
        {
            get
            {
                var address = GetProperty<Double>();
                return address;
            }
            set { SetProperty<Double>(value); }
        }

        [ParseFieldName("Longitude")]
        public Double Longitude
        {
            get
            {
                var address = GetProperty<Double>();
                return address;
            }
            set { SetProperty<Double>(value); }
        }

        public MapIcon GetMapIcon()
        {
            var pin = new MapIcon()
            {
                Location = this.Location,
                Title = this.ToiletName
            };

            return pin;

        }

    }
}
