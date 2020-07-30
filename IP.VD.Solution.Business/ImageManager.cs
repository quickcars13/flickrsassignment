using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IP.VD.Solution.Entity;

namespace IP.VD.Solution.Business
{
    public class ImageManager
    {
        #region Private constants declaration
        private static volatile ImageManager _instance = null;
        private static object _syncRoot = new object();

        private const string ConnectionString = "FlickrDataEntities";

        #endregion

        #region Constructors section
        /// <summary>
        /// 
        /// </summary>
        private ImageManager()
        {

        }
        #endregion

        #region Public Properties
        public static ImageManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ImageManager();
                        }
                    }
                }
                return _instance;
            }
        }
        #endregion


        #region Public methods



        /// <summary>
        /// check if image exists
        /// </summary>
        /// <param name="imageID"></param>
        /// <returns></returns>
        public bool CheckImageExists(string imageID)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
            bool result = false;
            result = (from r in dbContext.FlickrImages where r.PhotoId == imageID select r).Any();
            return result;
        }



        public void insertImage(FlickrImage image)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();

            if(!CheckImageExists(image.PhotoId))
            {
                dbContext.FlickrImages.Add(image);
                dbContext.SaveChanges();
            }
            
        }



        public List<FlickrImage> RetrieveFlickrImages()
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
            List<FlickrImage> listOfFlickrImages = null;
     

            listOfFlickrImages = (from f in dbContext.FlickrImages                                
                                select f).Distinct().ToList();

            return listOfFlickrImages;
        }

        public List<FlickrImage> RetrieveFlickrImages(string searchTerm)
        {
            FlickrDataEntities dbContext = new FlickrDataEntities();
            List<FlickrImage> listOfFlickrImages = null;


            listOfFlickrImages = (from f in dbContext.FlickrImages
                                      where f.Title.Contains(searchTerm) ||
                                      f.Description.Contains(searchTerm) ||
                                      f.Locality_Description.Contains(searchTerm) ||
                                      f.Region_Description.Contains(searchTerm) ||
                                      f.Tags.Contains(searchTerm) 
                                  select f).Distinct().ToList();

            return listOfFlickrImages;
        }
        #endregion


    }
}
