using IP.VD.Solution.Business;
using IP.VD.Solution.Entity;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IP.VD.Solution.Webservices.Controllers
{
  
    public class ImagesController : ApiController
    {
        
        [System.Web.Http.Route("api/images/{searchterm}")]
        public IHttpActionResult GetImagesBySearchTerm(string searchTerm)
        {
            try
            {
                List<FlickrImage> listofImages = ImageManager.Instance.RetrieveFlickrImages(searchTerm);

                if (listofImages == null)
                {
                    return NotFound();
                }
                return Ok(listofImages);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    return Ok(ex.Message + ex.InnerException.Message + ex.InnerException.StackTrace);
                else
                    return Ok(ex.Message + ex.StackTrace);
            }
        }
    }
}
