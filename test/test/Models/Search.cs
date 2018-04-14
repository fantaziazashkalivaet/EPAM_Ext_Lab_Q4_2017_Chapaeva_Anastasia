using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using test.DAL.Models;

namespace test.Models
{
    public class Search
    {
        public string Title { get; set; }

        public List<Tag> Tags { get; set; }
    }
}