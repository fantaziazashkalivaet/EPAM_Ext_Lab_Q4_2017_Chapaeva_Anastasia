using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.DAL.Models
{
    public class Document
    {
        public int DocumentID { get; set; }

        public string Title { get; set; }

        public byte[] Doc { get; set; }

        public int UserID { get; set; }

        public string UserName { get; set; }

        public DocumentAccess AccessID { get; set; }

        public int TagID { get; set; }

        public DateTime Date { get; set; }

        public string Hash { get; set; }
    }
}
