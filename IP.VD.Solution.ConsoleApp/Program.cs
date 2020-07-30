using FlickrNet;
using FoursquareApi;
using IP.VD.Solution.Business;
using IP.VD.Solution.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IP.VD.Solution.ConsoleApp
{
    class Program
    {
        private static string _flickrApiKey = "";
        private static string _foursquareClientId = "";
        private static string _foursquareClientSecretKey = "";
        static void Main(string[] args)
        {
            System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
            _flickrApiKey = configurationAppSettings.GetValue("FlickrApiKey", typeof(System.String)).ToString();
            _foursquareClientId = configurationAppSettings.GetValue("FourSquareClientID", typeof(System.String)).ToString();
            _foursquareClientSecretKey = configurationAppSettings.GetValue("FourSquareClientSecretKey", typeof(System.String)).ToString();

            var foursquare = new Foursquare(_foursquareClientId, _foursquareClientSecretKey);
            FoursquareApi.Models.FoursquareBaseModel<FoursquareApi.Models.Venues.SearchVenuesResponse> listOfVenues = foursquare.Venues.SearchVenues("Mauritius");
            
            Flickr flickr = new Flickr();
            flickr.ApiKey = _flickrApiKey;

            foreach (FoursquareApi.Models.Common.Venue.CompactVenue v in listOfVenues.Response.Venues)
            {
                var options = new PhotoSearchOptions { PerPage = 100, Page = 1, Latitude = v.Location.Lat, Longitude = v.Location.Lng };
                PhotoCollection photos = flickr.PhotosSearch(options);

                foreach (FlickrNet.Photo photo in photos)
                {
                    PhotoInfo pi = flickr.PhotosGetInfo(photo.PhotoId);

                    FlickrImage image = new FlickrImage();
                
                    image.DatePosted = pi.DatePosted;
                    image.DateTaken = pi.DateTaken;
                    image.Description = pi.Description;
                    image.Latitude = pi.Location.Latitude;
                    image.Longitude = pi.Location.Longitude;
                    image.OwnerUserId = pi.OwnerUserId;
                    image.PhotoId = pi.PhotoId;
                    image.Region_Description = retrieveRegionDescription(pi.Location);
                    image.Locality_Description = retrieveLocalityDescription(pi.Location);
                    image.Tags = retrieveTags(pi.Tags);
                    image.ThumbnailUrl = pi.ThumbnailUrl;
                    image.Title = pi.Title;
                    image.WebUrl = pi.WebUrl;

                    ImageManager.Instance.insertImage(image);
                }
            }

        }

     
        private static string retrieveRegionDescription(PlaceInfo pi)
        {
            string region_desc = string.Empty;
            if (pi != null && pi.Region!=null && !string.IsNullOrEmpty(pi.Region.Description))
            {
                region_desc = pi.Region.Description;
            }

            return region_desc;
        }

        private static string retrieveLocalityDescription(PlaceInfo pi)
        {
            string locality_desc = string.Empty;
            if (pi != null && pi.Locality != null && !string.IsNullOrEmpty(pi.Locality.Description))
            {
                locality_desc = pi.Locality.Description;
            }

            return locality_desc;
        }

        private static string retrieveTags(Collection<PhotoInfoTag> photoInfoTags)
        {
            string tags = string.Empty;

            if (photoInfoTags.Count > 0)
            {
                foreach(PhotoInfoTag ptag in photoInfoTags)
                {
                    tags += ptag.Raw + ",";
                }
            }

            return tags;
        }




    }
}
