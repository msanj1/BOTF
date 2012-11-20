using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace BOTF.Models
{
    public class LastFMVenue
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }

    public class LastFM
    {

     string apiKey;
     string host;
        public LastFM(string API, string Host )
        {
             apiKey = API;
             host = Host;
        
        }

        class SampleData
        {
            [JsonProperty(PropertyName = "items")]
            public System.Data.DataTable Items { get; set; }
        }

        public List<string> SearchVenues(string Venue)
        {
            string URL = host + "?method=venue.search&api_key=" + apiKey + "&venue=" + Venue + "&limit=5";
            var client = new WebClient();
            string xml = client.DownloadString(URL);
            List<string> tags = new List<string>();
            var xdoc = XDocument.Load(new StringReader(xml));
            List<string> venues = xdoc.Descendants("results").Elements("venuematches").Elements("venue").Elements("name").Select(c=>c.Value).ToList();
            return venues;
        }

        public List<string> GetSimilarArtists(string artistName) 
        {
            string URL = host + "?method=artist.getinfo&artist=" + artistName + "&api_key=" + apiKey + "&autocorrect=1";
            var client = new WebClient();
            string xml = client.DownloadString(URL);
            List<string> tags = new List<string>();
            var xdoc = XDocument.Load(new StringReader(xml));
            var names = xdoc.Descendants("artist").Elements("similar").Elements("artist").Elements("name").Select(r => r.Value).Take(5).ToList();
            return names;
        }

        public LastFMVenue GetVeneuGeo(string venue)
        {
            //string URL = host + "?method=venue.search&venue=" + venue + "&api_key=" + apiKey + "&format=json&limit=1";
            string URL = "http://maps.googleapis.com/maps/api/geocode/json?address=" + venue + "&sensor=false";
            var client = new WebClient();
            string json =  client.DownloadString(URL);
            JavaScriptSerializer jss = new JavaScriptSerializer();
            var data =  jss.Deserialize<dynamic>(json);
            if (data["status"] == "OK")
            {
                var tmp = data["results"];

                tmp = tmp[0]["geometry"];
                tmp = tmp["location"];
                string latitude = tmp["lat"].ToString();
                string longitude = tmp["lng"].ToString();
                return new LastFMVenue { Latitude = latitude, Longitude = longitude };
            }

            return new LastFMVenue { };
            
          
                
        }

        public dynamic GetArtistInfo( string artistName)
        {
          
            string URL = host + "?method=artist.getinfo&artist=" + artistName + "&api_key=" + apiKey + "&autocorrect=1";
            var client = new WebClient();
            string xml = client.DownloadString(URL);


            
            List<string> tags = new List<string>();

            var xdoc = XDocument.Load(new StringReader(xml));
            var data = from item in xdoc.Descendants("artist")
                       select new
                       {
                           artist = item.Element("name").Value,
                           image = item.Element("image").Attribute("small").Value,
                       };
            var correctname = xdoc.Descendants("artist").Elements("name").Select(r => r.Value).ToArray();
            var tagdata = xdoc.Descendants("artist").Elements("tags").Elements("tag").Elements("name").Select(r => r.Value).ToArray();
            var imgs = xdoc.Descendants("artist").Elements("image")
               .Where(node => (string)node.Attribute("size") == "mega")
               .Select(node => node.Value.ToString())
               .ToList();
            var artistbio = xdoc.Descendants("artist").Elements("bio").Elements("summary").Select(r => r.Value).Take(1).ToArray();
            var genre = xdoc.Descendants("artist").Elements("tags").Elements("tag").Elements("name").Select(r => r.Value).Take(1).ToArray();
            string image = "";
            string bio = "";
            string Genre = "";

           
           
            image = imgs[0];
            bio = artistbio[0];
            Genre = genre[0];

            return new { PictureURL = image, ArtistBio = bio, MusicGenre = Genre, ArtistName = correctname[0] };
        }

        public List<string> SearchArtist(string artist)
        {
            string url = host + "?method=artist.search&artist=" + artist + "&api_key=" + apiKey + "&limit=5";
            var client = new WebClient();
            string xml = client.DownloadString(url);

            List<string> names = new List<string>();
            XmlTextReader reader = new XmlTextReader(new StringReader(xml));
            reader.MoveToContent();
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "name")
                    {
                        XElement el = XNode.ReadFrom(reader) as XElement;
                        if (el != null)
                        {
                            names.Add(el.Value);
                        }
                    }
                }
            }
            return names;
        }
    }
}