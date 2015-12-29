using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;


namespace facedetection_cdn.Controllers
{
    public class ImageController : ApiController
    {
        //
        // GET: /api/image/

      public HttpResponseMessage Get(string id)
      {
        string[] reqParams = id.Split(new char[] { '_' },2);
        
        string dataset = AssetsController._DATADIR + "\\" + reqParams[0] + "\\";
            
        var imagePath = from file in Models.MyDirectory.GetFiles(dataset, reqParams[1], SearchOption.AllDirectories)
            select file;
        
        if (imagePath.Count() == 0) return new HttpResponseMessage(HttpStatusCode.NotFound);

            
            byte[] fileData = System.IO.File.ReadAllBytes(imagePath.First());
            MemoryStream ms = new MemoryStream(fileData);
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(fileData);
            response.Content.Headers.ContentLength = fileData.Length;
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                Public = true,
                MaxAge = new TimeSpan(1, 0, 0, 0)
            };
            string format = Path.GetExtension(reqParams[1]).Substring(1);

            var regex = new Regex(AssetsController._IMAGEFORMATS, RegexOptions.IgnoreCase);
            if (regex.IsMatch(reqParams[1]))
            {
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + format);
            }
            else
            {
                switch (format)
                {
                    case "txt":
                    case "eye":
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                        break;
                    default:
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        break;
                }

            }


            return response;
      }

    }
}
