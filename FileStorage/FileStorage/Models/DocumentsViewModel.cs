namespace FileStorage.Models
{
    using System.Collections.Generic;
    using DAL.Models;

    public class DocumentsViewModel
    {
        public IEnumerable<Document> Documents { get; set; }

        public string Title { get; set; }

        public List<Tag> Tags { get; set; }

        public List<string> Filter { get; set; } = new List<string>();

        public PageInfo PageInfo { get; set; }
    }
}