using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooFinder.Models
{
    public sealed class ParseHelper
    {
        ParseConfig config = null;
        ParseGeoPoint currentLocation;
        static ParseHelper instance=null;
        const string toiletModelName = "Toilet";
        private ObservableCollection<Toilet> _parseToilets = new ObservableCollection<Toilet>();
        private int skipCount = 20;

        public ObservableCollection<Toilet> parseToilets {
            get
            {
                return _parseToilets;
            }
        }

        ParseHelper()
        {
        }

        public static ParseHelper Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new ParseHelper();
                }
                return instance;
            }
        }

        public async Task getNearbyToilets(ParseGeoPoint location, Boolean shouldSkip)
        {
            if (currentLocation.Latitude != location.Latitude || currentLocation.Longitude != location.Longitude)
            {
                //New location, so empty data
                _parseToilets.Clear();
                currentLocation = location;
            } 

            var query = new ParseQuery<Toilet>();
            query = query.WhereNear("Location", location);
            query = query.Limit(20);

            if (shouldSkip)
            {
                query = query.Skip(skipCount);
                skipCount += 20;
            }

            IEnumerable<Toilet> result = await query.FindAsync();

            if (shouldSkip)
            {
                result.ToList().ForEach(_parseToilets.Add);
            } else
            {
                _parseToilets.Clear();
                result.ToList().ForEach(_parseToilets.Add);
            }

            foreach (Toilet toilet in _parseToilets)
            {
                toilet.searchLocationPoint = location;
            }
        }

        public async Task<String> getParseConfigElement(string keyName)
        {
            try
            {
                config = await ParseConfig.GetAsync();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                config = ParseConfig.CurrentConfig;
            }

            String parseConfigText = null;
            bool searchResult = config.TryGetValue<String>(keyName, out parseConfigText);

            if (searchResult)
            {
                return parseConfigText;
            } else
            {
                return null;
            }
        }
    }
}
