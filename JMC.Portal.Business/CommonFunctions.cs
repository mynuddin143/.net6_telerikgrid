//using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//using JMC.Portal.Business.AtlasSAPPortal;
using System.Configuration;

namespace JMC.Portal.Business
{
    public partial class CommonFunctions
    {
        private string MapPath(string path)
        {
            var contentRootPath = (string)AppDomain.CurrentDomain.GetData("ContentRootPath");

            string strPath = Path.Combine((string)AppDomain.CurrentDomain.GetData("ContentRootPath"), path);

            return strPath;
        }
    }
}
