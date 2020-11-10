using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Funeral.Web.ConvertToPdf
{
    public partial class ConvertCurrentPageToPdf
    {
        //protected override void Render(HtmlTextWriter writer)
        //{
            
        //        // get html of the page
        //        TextWriter myWriter = new StringWriter();
        //        HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
        //        base.Render(htmlWriter);

        //        // instantiate a html to pdf converter object
        //        HtmlToPdf converter = new HtmlToPdf();

        //        // create a new pdf document converting the html string of the page
        //        PdfDocument doc = converter.ConvertHtmlString(
        //            myWriter.ToString(), Request.Url.AbsoluteUri);

        //        // save pdf document
        //        doc.Save(Response, false, "Sample.pdf");

        //        // close pdf document
        //        doc.Close();
            
        //}
    }
}
