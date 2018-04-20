namespace FileStorage.Models
{
    using System.Collections.Generic;
    using DAL.Models;

    public class AccessViewModel
    {
        public int DocumentID { get; set; }

        public DocumentAccess OldAccess { get; set; }

        public DocumentAccess NewAccess { get; set; }

        public IEnumerable<DocumentAccess> ListAccess { get; set; }
    }
}