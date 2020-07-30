using FlickrNet;
using FoursquareApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {

        private string FOURSQUARE_CLIENTID = "SXQ5WETX41QKSXXQOV2LTPPGIVOTR2XRHCEAOPAU5SFO1FEY";
        private string FOURSQUARE_CLIENTSECRET = "3HCXRHCGC2PAS1YSV0ZNBTZKUVKS2ZSLQNLMOMNUBID3ARKH";
        private string FLICKR_API_KEY = "6bd04d2d71c96d60ee2ddd16dc26b0aa";

        protected void Page_Load(object sender, EventArgs e)
        {

            var foursquare = new Foursquare(FOURSQUARE_CLIENTID, FOURSQUARE_CLIENTSECRET);

            FoursquareApi.Models.FoursquareBaseModel<FoursquareApi.Models.Venues.SearchVenuesResponse> listOfVenues = foursquare.Venues.SearchVenues("Mauritius");

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("near", "Mauritius");
            

            Flickr flickr = new Flickr();
            flickr.ApiKey = FLICKR_API_KEY;

            foreach (FoursquareApi.Models.Common.Venue.CompactVenue v in listOfVenues.Response.Venues)
            {

                var options = new PhotoSearchOptions { PerPage = 20, Page = 1, Latitude = v.Location.Lat, Longitude = v.Location.Lng };
                PhotoCollection photos = flickr.PhotosSearch(options);

                foreach (FlickrNet.Photo photo in photos)
                {
                    PhotoInfo pi =  flickr.PhotosGetInfo(photo.PhotoId);
                    
                    lblData.InnerHtml += "<img src='" + photo.Small320Url + "'><br />";


                    FlickrDBModel dbContext = new FlickrDBModel();

                }
            }






        }
    }
}