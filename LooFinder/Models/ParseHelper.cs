using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LooFinder.Models
{
    public sealed class ParseHelper
    {
        ParseConfig config = null;
        static ParseHelper instance=null;
        const string toiletModelName = "Toilet";
        private List<Toilet> _parseToilets = new List<Toilet>();

        public List<Toilet> parseToilets {
            get
            {
                return this._parseToilets;
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

        public async Task getNearbyToilets(ParseGeoPoint location, Dictionary<String, bool> options)
        {

            var query = new ParseQuery<Toilet>();
            query = query.WhereNear("Location", location);

            foreach (KeyValuePair<String, bool> entry in options) {
                query = query.WhereEqualTo(entry.Key, entry.Value);
            }
            
            query = query.Limit(10);
            IEnumerable<Toilet> result = await query.FindAsync();
            _parseToilets = result.ToList();

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
