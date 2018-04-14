using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace test.Models
{
    public class Document
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public byte[] Doc { get; set; }

        public int UserID { get; set; }

        public int AccessID { get; set; }

        public int TagID { get; set; }

        public DateTime Date { get; set; }

        public List<char> Hash { get; set; }
    }
}