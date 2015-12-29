using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using facedetection_cdn.Models;



namespace facedetection_cdn.Controllers
{
    public class AssetsController : ApiController
    {
        //
        // GET api/assets
        public static string _DATADIR = WebConfigurationManager.AppSettings.Get("DATADIR");
        public static string _IMAGEFORMATS = "\\.jpg|\\.jpeg|\\.png|\\.gif|\\.pgm";


        public string[] Get()
        {
          var dirs = Directory.EnumerateDirectories(_DATADIR);
          return dirs.ToArray();
        }

        // GET api/assets/lfw
        public string[] Get(string id)
        {
          string path = _DATADIR+"\\"+id;
          if(Directory.Exists(path))
          {
                var files = from file in MyDirectory.GetFiles(path, AssetsController._IMAGEFORMATS, SearchOption.AllDirectories)
                            select id + "_" + Path.GetFileName(file);                
                return files.ToArray();
          }
          else
          {
            return new string[]{"Directory does not exists"};
          }
        }
    }
}
