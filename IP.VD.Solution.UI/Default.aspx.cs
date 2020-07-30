using IP.VD.Solution.Business;
using IP.VD.Solution.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IP.VD.Solution.UI
{
    public partial class _Default : Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                BindData();
            }

        }


        private static string FormatDataRow(FlickrImage image)
        {
            string output = string.Empty;

            string additionalTagLines = string.Empty;

            string ImageURL = image.ThumbnailUrl.Replace("_t.", ".");

            if (!string.IsNullOrEmpty(image.Tags))
            {
                string[] tags = image.Tags.Split(',');
                additionalTagLines = "<div style='word-wrap:break-word'> Tags: ";
                foreach (string tag in tags)
                {
                   
                    if (!string.IsNullOrEmpty(tag))
                        additionalTagLines +=  tag + ",";

                }
                additionalTagLines += "</div>";
                additionalTagLines = additionalTagLines.Replace("\n", "\n\n");

            }

            string line1 = "<section data-padding='bottom'>";
            string line2 = "<article class='rectangle-element--article--inline--boxed--shadow' data-padding>";
            string line3 = "<div class='info'>";
            string line4 = "<div class='image'>";
            string line5 = "<a class='example-image-link' href='"+ ImageURL + "' data-lightbox='example-set'><img class='example-image' src='"+ ImageURL + "' alt='" + image.Title + "' /></a>";
            string line6 = "</div>";
            string line7 = "<div class='text'>";
            string line8 = "<div class='meta'>";
            string line9 = "<div class='tags'>";
            string line10 = "<time class='e-date' datetime='2020-07-05T07:55:21+02:00'>" + image.DatePosted + "</time>";
            string line11 = "<ul class='categories'>";
            string line12 = "<li>"+image.Region_Description+"</li>";
            string line13 = "";
            string line14 = "</ul>";
            string line15 = "</div>";
            string line16 = "</div>";
            string line17 = "<h3>" + image.Description + "</h3>";
            if (line17.Length > 200)
            {
                line17 = line17.Substring(0, 200);
                line17 += "<h4> <a href='"+ image.WebUrl + "' target='_blank'>Read More </a></h4>";
            }
            string line18 = additionalTagLines;
            string line19 = "</div>";
            string line20 = "</div>";
            string line21 = "</article>";
            string line22 = "</section>";

            output = line1 + line2 + line3 + line4 + line5 + line6 + line7 + line8 + line9 + line10 + line11 + line12 + line13 + line14 + line15 + line16 + line17 + line18 + line19 + line20 + line21 + line22;

            return output;

        }
        protected void GV_ImageResults_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GV_ImageResults_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_ImageResults.PageIndex = e.NewPageIndex;
            BindData();
        }

        private void BindData()
        {
            List<FlickrImage> listofImages = ImageManager.Instance.RetrieveFlickrImages();


            DataTable dt = new DataTable();
            dt.Columns.Add("ImageField");

            foreach (FlickrImage fimage in listofImages)
            {
                DataRow dr = dt.NewRow();
                dr[0] = FormatDataRow(fimage);
                dt.Rows.Add(dr);
            }

            GV_ImageResults.DataSource = dt;
            GV_ImageResults.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = searchTextBox.Text;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                List<FlickrImage> listofImages = ImageManager.Instance.RetrieveFlickrImages(searchTerm);

                DataTable dt = new DataTable();
                dt.Columns.Add("ImageField");

                foreach (FlickrImage fimage in listofImages)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = FormatDataRow(fimage);
                    dt.Rows.Add(dr);
                }

                GV_ImageResults.DataSource = dt;
                GV_ImageResults.DataBind();
            }
            else
            {
                BindData();
            }
        }


    }
}