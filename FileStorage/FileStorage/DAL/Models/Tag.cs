using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileStorage.DAL.Models
{
    public class Tag
    {
        public int TagID { get; set; }

        public string TagName { get; set; }

        public string Type { get; set; }
    }
}