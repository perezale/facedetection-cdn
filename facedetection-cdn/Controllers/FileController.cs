using facedetection_cdn.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace facedetection_cdn.Controllers
{
    public class FileController : ApiController
    {
        //
        // GET api/file            
        public string[] Get()
        {
            var dirs = Directory.EnumerateDirectories(AssetsController._DATADIR);
            return dirs.ToArray();
        }

        // GET api/assets/lfw
        public string[] Get(string id)
        {
            var baseUri = Request.RequestUri.GetLeftPart(UriPartial.Authority) + System.Web.HttpRuntime.AppDomainAppVirtualPath+ "/data/" + id + "/";
            //var baseUri = Request.RequestUri.GetLeftPart(UriPartial.Authority) + Url.Content("~") + "/data/"+id+"/";
            string path = AssetsController._DATADIR + Path.DirectorySeparatorChar + id;
            if (Directory.Exists(path))
            {
                var files = from file in MyDirectory.GetFiles(path, AssetsController._IMAGEFORMATS, SearchOption.AllDirectories)
                            select baseUri + Path.GetFullPath(file).Replace(path+ Path.DirectorySeparatorChar, "").Replace(@"\","/");
                return files.ToArray();
            }
            else
            {
                return new string[] { "Directory does not exists" };
            }
        }

    }
}